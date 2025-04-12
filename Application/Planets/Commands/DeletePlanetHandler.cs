using JediArchives.DataStorage;
using MediatR;

namespace JediArchives.Application.Planets.Commands;

public class DeletePlanetHandler(DataContextWrite context) : IRequestHandler<DeletePlanetCommand, bool> {
    private readonly DataContextWrite _context = context;

    public async Task<bool> Handle(DeletePlanetCommand request, CancellationToken cancellationToken) {
        try {
            var planet = await _context.Planets.FindAsync(request.Id);

            ArgumentNullException.ThrowIfNull(planet, nameof(planet));

            _context.Planets.Remove(planet);

            await _context.SaveChangesAsync();

            return true;
        } catch (Exception) {
            return false;
        }
    }
}
