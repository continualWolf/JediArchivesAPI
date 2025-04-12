using MediatR;

namespace JediArchives.Application.Planets.Commands;

public class DeletePlanetCommand : IRequest<bool> {
    public int Id { get; set; }

    public DeletePlanetCommand(int id) {
        Id = id;
    }
}
