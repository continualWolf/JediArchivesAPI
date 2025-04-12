using JediArchives.Application.Planets.Models;
using JediArchives.Application.Users.Models;
using JediArchives.Application.Users.Queries;
using JediArchives.DataStorage;
using MediatR;

namespace JediArchives.Application.Planets.Queries;

public class GetPlanetByIdHandler(DataContextRead context) : IRequestHandler<GetPlanetByIdQuery, PlanetResponse?> {
    private readonly DataContextRead _context = context;

    public async Task<PlanetResponse?> Handle(GetPlanetByIdQuery request, CancellationToken cancellationToken) {
        var user = await _context.Planets.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user is null) {
            return null;
        }

        return PlanetResponse.FromEF(user);
    }
}