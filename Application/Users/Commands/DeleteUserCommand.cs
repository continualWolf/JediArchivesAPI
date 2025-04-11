using MediatR;

namespace JediArchives.Application.Users.Commands;
public class DeleteUserCommand : IRequest<bool> {
    public int Id { get; set; }

    public DeleteUserCommand(int id) {
        Id = id;
    }
}
