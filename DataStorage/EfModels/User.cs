namespace JediArchives.DataStorage.EfModels;
public class User {
    public int Id { get; set; }
    public string Name { get; set; }
    public string HashedPassword { get; set; }
    public JediRanks Rank { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

public enum JediRanks {
    Youngling,
    Padawan,
    Knight,
    Master,
    GrandMaster,
    Archivist,
    CouncilMember
}
