using JediArchives.DataStorage.EfModels;

namespace JediArchives.Application.Users.Models;
public record LoginResponse(string token, int Id, string Name, JediRanks Rank, DateTime DateOfBirth) {
    public static LoginResponse FromEF(string token, User user) {
        return new LoginResponse(token, user.Id, user.Name, user.Rank, user.DateOfBirth);
    }
};
