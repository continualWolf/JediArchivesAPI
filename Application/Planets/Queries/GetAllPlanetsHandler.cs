using JediArchives.Application.Planets.Models;
using JediArchives.DataStorage;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JediArchives.Application.Planets.Queries;

public class GetAllPlanetsHandler(DataContextRead dataContext) : IRequestHandler<GetAllPlanetsQuery, List<PlanetResponse>> {
    private readonly DataContextRead _context = dataContext;

    public async Task<List<PlanetResponse>> Handle(GetAllPlanetsQuery request, CancellationToken cancellationToken) {
        var query = _context.Planets
            .Include(x => x.System)
            .AsNoTracking()
            .AsQueryable();

        // Filtering
        if (!string.IsNullOrWhiteSpace(request.System))
            query = query.Where(p => p.System.Name == request.System);

        // Sorting
        query = request.Sort?.ToLower() switch {
            "name" => query.OrderBy(p => p.Name),
            "name_desc" => query.OrderByDescending(p => p.Name),
            _ => query.OrderBy(p => p.Name)
        };

        var result = await query.ToListAsync(cancellationToken);

        return result.Select(PlanetResponse.FromEF).ToList();
    }
}
