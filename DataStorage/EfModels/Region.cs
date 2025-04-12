namespace JediArchives.DataStorage.EfModels;

public class Region {
    public int Id { get; set; }
    public string Name { get; set; }

    public int? PolygonId { get; set; }
    public Polygon? Polygon { get; set; }

    public ICollection<Sector> Sectors { get; set; }
}
