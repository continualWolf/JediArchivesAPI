using JediArchives.DataStorage.EfModels;

namespace JediArchives.Services.Interfaces;
public interface IJwtService {
    string GenerateToken(int userId, string userName, JediRanks rank);
}
