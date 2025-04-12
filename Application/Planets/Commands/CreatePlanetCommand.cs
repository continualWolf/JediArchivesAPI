using JediArchives.Application.Planets.Models;
using JediArchives.DataStorage.EfModels;
using MediatR;

namespace JediArchives.Application.Planets.Commands;

public class CreatePlanetCommand : IRequest<PlanetResponse> {
    public string Name { get; set; }
    public Allegiance Allegiance { get; set; }
    public float? OrbitX { get; set; }
    public float? OrbitY { get; set; }
    public int SystemEntityId { get; set; }

    public Planet ToEF() => new() {
        Name = Name,
        Allegiance = Allegiance,
        OrbitX = OrbitY,
        SystemEntityId = SystemEntityId,
    };
}
