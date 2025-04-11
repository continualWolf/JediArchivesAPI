using JediArchives.Application.Users.Models;
using JediArchives.DataStorage.EfModels;
using MediatR;

namespace JediArchives.Application.Users.Commands;

public class CreateUserCommand : IRequest<UserResponse> {
    public string Name { get; set; }
    public JediRanks Rank { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Password { get; set; }

    public DataStorage.EfModels.User ToEF() => new() {
        Name = Name,
        Rank = Rank,
        DateOfBirth = DateOfBirth,
        HashedPassword = Password
    };
}