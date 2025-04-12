namespace JediArchives.DataStorage.EfModels;

public class Planet {
    public int Id { get; set; }

    public string Name { get; set; }

    public Allegiance Allegiance { get; set; }

    public float? OrbitX { get; set; }
    public float? OrbitY { get; set; }

    public int SystemEntityId { get; set; }
    public SystemEntity System { get; set; }
}


public enum Allegiance {
    GalacticRepublic,
    JediOrder,
    TradeFederation,
    TechnoUnion,
    TheHutts,
    Separatist,
    Mandolorians
}