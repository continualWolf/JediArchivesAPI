using JediArchives.Application.Planets.Models;
using MediatR;

namespace JediArchives.Application.Planets.Queries;

public class GetPlanetByIdQuery : IRequest<PlanetResponse> {
    public int Id { get; set; }

    public GetPlanetByIdQuery(int id) {
        Id = id;
    }
}