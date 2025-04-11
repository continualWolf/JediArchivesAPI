using JediArchives.DataStorage.EfModels;

namespace JediArchives.Application.Users.Models;
public record CreateUserRequest(string Name, JediRanks Rank, DateTime DateOfBirth, string Password);
