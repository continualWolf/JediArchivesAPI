using JediArchives.Application.Planets.Models;
using MediatR;

namespace JediArchives.Application.Planets.Queries;

public class GetAllPlanetsQuery: IRequest<List<PlanetResponse>> {
    public string? Sort { get; set; } = "name";
    public string? System { get; set; }
}
