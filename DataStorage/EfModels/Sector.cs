namespace JediArchives.DataStorage.EfModels;

public class Sector {
    public int Id { get; set; }

    public string Name { get; set; }

    // Link to the region it's in
    public int RegionId { get; set; }
    public Region Region { get; set; }

    // Optional: border definition for mapping
    public int? PolygonId { get; set; }
    public Polygon? Polygon { get; set; }

    public ICollection<SystemEntity> Systems { get; set; } = new List<SystemEntity>();
}
