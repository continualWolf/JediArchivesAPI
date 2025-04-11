using JediArchives.DataStorage;
using MediatR;

namespace JediArchives.Application.Users.Commands;
public class DeleteUserHandler(DataContextWrite context) : IRequestHandler<DeleteUserCommand, bool> {
    private readonly DataContextWrite _context = context;

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
        try {
            var user = await _context.Users.FindAsync(request.Id);

            ArgumentNullException.ThrowIfNull(user, nameof(user));

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        } catch (Exception) {
            return false;
        }
    }
}
