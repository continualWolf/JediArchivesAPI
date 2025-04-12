using JediArchives.DataStorage.EfModels;

namespace JediArchives.Application.Planets.Models;

public record PlanetResponse(int Id, string Name, Allegiance Allegiance, float? OrbitX, float? OrbitY, int SystemEntityId) {

    public static PlanetResponse FromEF(Planet planet) {
        return new PlanetResponse(
        planet.Id,
        planet.Name,
        planet.Allegiance,
        planet.OrbitX,
        planet.OrbitY,
        planet.SystemEntityId);
    }
};