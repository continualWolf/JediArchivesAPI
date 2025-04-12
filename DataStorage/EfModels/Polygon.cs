namespace JediArchives.DataStorage.EfModels;

public class Polygon {
    public int Id { get; set; }

    public ICollection<CoordinatePoint> Points { get; set; } = new List<CoordinatePoint>();
}
