using JediArchives.Application.Planets.Commands;
using JediArchives.Application.Planets.Queries;
using JediArchives.DataStorage.EfModels;
using JediArchives.Helper;
using JediArchives.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JediArchives.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetsController(IJwtService jwtService, IMediator mediator) : ApiBaseController {
    private readonly IMediator _mediator = mediator;
    private readonly IJwtService _jwtService = jwtService;

    /// <summary>
    /// Retrieves a planet by its unique ID.
    /// </summary>
    /// <param name="Id">The ID of the planet to retrieve.</param>
    /// <returns>Returns 200 OK with the planet data, or 404 if not found.</returns>
    [HttpGet("{Id}")]
    [JediAuthorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Get(int id) {
        return await Execute(() => _mediator.Send(new GetPlanetByIdQuery(id)));
    }

    /// <summary>
    /// Creates a new planet in the Jedi Archives galaxy.
    /// </summary>
    /// <param name="command">The data used to create the planet.</param>
    /// <returns>Returns 201 Created with the new planet.</returns>
    [HttpPost]
    [JediAuthorize(JediRanks.Archivist, JediRanks.CouncilMember)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create(CreatePlanetCommand command) {
        return await Execute(async () => {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        });
    }

    /// <summary>
    /// Deletes a planet from the Jedi Archives.
    /// </summary>
    /// <param name="Id">The ID of the planet to delete.</param>
    /// <returns>Returns 204 No Content if successful, or 500 if the operation fails.</returns>
    [HttpDelete("{Id}")]
    [JediAuthorize(JediRanks.Archivist, JediRanks.CouncilMember)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id) {
        return await Execute(() => _mediator.Send(new DeletePlanetCommand(id)));
    }

    /// <summary>
    /// Updates an existing planet in the Jedi Archives.
    /// </summary>
    /// <param name="Id">The ID of the planet to update.</param>
    /// <param name="command">The updated planet data.</param>
    /// <returns>Returns 204 No Content if successful, or 500 if the operation fails.</returns>
    [HttpPut("{Id}")]
    [JediAuthorize(JediRanks.Archivist, JediRanks.CouncilMember)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(int id, UpdatePlanetCommand command) {
        command.Id = id;
        return await Execute(() => _mediator.Send(command));
    }
}