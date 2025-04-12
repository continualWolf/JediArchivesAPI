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
public class PlanetsController(IJwtService jwtService, IMediator mediator) : ControllerBase {
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
    public async Task<IActionResult> Get(int Id) {
        var planet = await _mediator.Send(new GetPlanetByIdQuery(Id));

        if (planet is null) {
            return NotFound(new ProblemDetails {
                Title = "Planet Not Found",
                Detail = $"The planet with ID {Id} does not exist.",
                Status = 404,
                Instance = HttpContext.Request.Path
            });
        }

        return Ok(planet);
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
        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
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
    public async Task<IActionResult> Delete(int Id) {
        var result = await _mediator.Send(new DeletePlanetCommand(Id));

        if (!result) {
            return StatusCode(500, new ProblemDetails {
                Title = "Internal Server Error",
                Detail = "There has been an unknown internal error.",
                Status = 500,
                Instance = HttpContext.Request.Path
            });
        }

        return NoContent();
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
    public async Task<IActionResult> Update(int Id, UpdatePlanetCommand command) {
        command.Id = Id;

        var result = await _mediator.Send(command);

        if (!result) {
            return StatusCode(500, new ProblemDetails {
                Title = "Internal Server Error",
                Detail = "There has been an unknown internal error.",
                Status = 500,
                Instance = HttpContext.Request.Path
            });
        }

        return NoContent();
    }
}