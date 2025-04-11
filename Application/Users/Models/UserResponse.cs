using JediArchives.DataStorage.EfModels;

namespace JediArchives.Application.Users.Models;
public record UserResponse(int Id, string Name, JediRanks Rank, DateTime DateOfBirth) {

    public static UserResponse FromEF(User user) {
        return new UserResponse(
        user.Id,
        user.Name,
        user.Rank,
        user.DateOfBirth);
    }
};
