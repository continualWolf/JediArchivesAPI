using JediArchives.DataStorage.EfModels;
using Microsoft.AspNetCore.Mvc;

namespace JediArchives.Application.Planets.Models;
public record CreatePlanetRequest(string Name, Allegiance Allegiance, float OrbitX, float OrbitY, int SystemEntityId);