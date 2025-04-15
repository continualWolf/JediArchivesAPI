using JediArchives.Application.Users.Commands;
using JediArchives.Application.Users.Models;
using JediArchives.Application.Users.Queries;
using JediArchives.DataStorage.EfModels;
using JediArchives.Helper;
using JediArchives.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JediArchives.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IJwtService jwtService, IMediator mediator) : ControllerBase {
    private readonly IMediator _mediator = mediator;
    private readonly IJwtService _jwtService = jwtService;

    /// <summary>
    /// Authenticates a Jedi using their ID and password, returning a JWT token and user details if successful.
    /// </summary>
    /// <param name="query">The login credentials containing the user ID and password.</param>
    /// <returns>
    /// Returns 200 OK with a JWT token and user data if authentication succeeds, 
    /// or 401 Unauthorized if the credentials are invalid.
    /// </returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginQuery query) {
        return Ok(await _mediator.Send(query));
    }

    /// <summary>
    /// Retrieves details of the currently authenticated Jedi from their JWT token.
    /// </summary>
    /// <returns>
    /// Returns 200 OK with the current user's ID, name, and role extracted from the token.
    /// </returns>
    [HttpGet("me")]
    [JediAuthorize]
    public IActionResult Me() {
        var id = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var name = User.FindFirstValue(ClaimTypes.Name);
        var role = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new { id, name, role });
    }

    /// <summary>
    /// Registers a new Jedi in the Jedi Archives.
    /// </summary>
    /// <param name="command">The user details used to create the new Jedi.</param>
    /// <returns>
    /// Returns 201 Created with the created user's data and a route to retrieve them.
    /// Requires Archivist, CouncilMember, Master, or GrandMaster rank.
    /// </returns>
    [HttpPost]
    [JediAuthorize(JediRanks.Archivist, JediRanks.CouncilMember, JediRanks.Master, JediRanks.GrandMaster)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(CreateUserCommand command) {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Retrieves a Jedi by their unique ID.
    /// </summary>
    /// <param name="id">The ID of the Jedi to retrieve.</param>
    /// <returns>
    /// Returns 200 OK with the Jedi's data if found, or 404 Not Found if no Jedi exists with the specified ID.
    /// </returns>
    [HttpGet("{id:int}")]
    [JediAuthorize]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id) {
        return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
    }

    /// <summary>
    /// Deletes a Jedi from the Archives.
    /// </summary>
    /// <param name="id">The ID of the Jedi to delete.</param>
    /// <returns>
    /// Returns 204 No Content if the Jedi was deleted successfully, 
    /// or 500 Internal Server Error if the deletion fails unexpectedly.
    /// Only Archivists are authorized to perform this action.
    /// </returns>
    [HttpDelete("{id}")]
    [JediAuthorize(JediRanks.Archivist)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUser(int id) {
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Updates the data of an existing Jedi in the Archives.
    /// </summary>
    /// <param name="id">The ID of the Jedi to update.</param>
    /// <param name="command">The updated Jedi details.</param>
    /// <returns>
    /// Returns 204 No Content if the update succeeds, or 500 Internal Server Error if something goes wrong.
    /// Requires Archivist, CouncilMember, Master, or GrandMaster rank.
    /// </returns>
    [HttpPut("{id}")]
    [JediAuthorize(JediRanks.Archivist, JediRanks.CouncilMember, JediRanks.Master, JediRanks.GrandMaster)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserCommand command) {
        command.Id = id;
        await _mediator.Send(command);
        return NoContent();
    }
}