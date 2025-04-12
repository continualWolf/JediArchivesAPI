using JediArchives.DataStorage;
using MediatR;

namespace JediArchives.Application.Planets.Commands;

public class UpdatePlanetHandler(DataContextWrite context) : IRequestHandler<UpdatePlanetCommand, bool> {
    private readonly DataContextWrite _contextWrite = context;

    public async Task<bool> Handle(UpdatePlanetCommand request, CancellationToken cancellationToken) {
        try {
            var planet = await _contextWrite.Planets.FindAsync(request.Id);

            ArgumentNullException.ThrowIfNull(planet, nameof(request));

            planet.Name = request.Name;
            planet.OrbitX = request.OrbitX;
            planet.OrbitY = request.OrbitY;
            planet.SystemEntityId = request.SystemEntityId;
            planet.Allegiance = request.Allegiance;

            _contextWrite.SaveChanges();

            return true;
        } catch (Exception) {
            return false;
        }
    }
}
