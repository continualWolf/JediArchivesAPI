using JediArchives.DataStorage.EfModels;
using MediatR;

namespace JediArchives.Application.Planets.Commands;

public class UpdatePlanetCommand : IRequest<bool> {
    public int Id { get; set; }
    public string Name { get; set; }
    public Allegiance Allegiance { get; set; }
    public float? OrbitX { get; set; }
    public float? OrbitY { get; set; }
    public int SystemEntityId { get; set; }
}
