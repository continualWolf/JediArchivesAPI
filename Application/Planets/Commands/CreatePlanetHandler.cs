using JediArchives.Application.Planets.Models;
using JediArchives.DataStorage;
using MediatR;

namespace JediArchives.Application.Planets.Commands;

public class CreatePlanetHandler(DataContextWrite context) : IRequestHandler<CreatePlanetCommand, PlanetResponse> {
    private readonly DataContextWrite _context = context;

    public async Task<PlanetResponse> Handle(CreatePlanetCommand request, CancellationToken cancellationToken) {
        var planet = request.ToEF();

        await _context.Planets.AddAsync(planet, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return PlanetResponse.FromEF(planet);
    }
}
