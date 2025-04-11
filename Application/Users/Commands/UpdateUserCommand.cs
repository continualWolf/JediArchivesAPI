using JediArchives.DataStorage.EfModels;
using MediatR;

namespace JediArchives.Application.Users.Commands;
public class UpdateUserCommand : IRequest<bool> {
    public int Id { get; set; }
    public string Name { get; set; }
    public JediRanks Rank { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Password { get; set; }
}
