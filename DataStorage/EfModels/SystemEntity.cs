namespace JediArchives.DataStorage.EfModels;

public class SystemEntity {
    public int Id { get; set; }

    public string Name { get; set; }

    public float X { get; set; }
    public float Y { get; set; }

    public int SectorId { get; set; }
    public Sector Sector { get; set; }

    public ICollection<Planet> Planets { get; set; } = new List<Planet>();
}
