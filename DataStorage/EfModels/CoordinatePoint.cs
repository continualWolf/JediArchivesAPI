namespace JediArchives.DataStorage.EfModels;

public class CoordinatePoint {
    public int Id { get; set; }

    public float X { get; set; }
    public float Y { get; set; }

    public int Order { get; set; }  // Defines drawing order

    public int PolygonId { get; set; }
    public Polygon Polygon { get; set; }
}
