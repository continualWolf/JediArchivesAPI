using JediArchives.DataStorage;
using MediatR;

namespace JediArchives.Application.Users.Commands;

public class UpdateUserHandler(DataContextWrite context) : IRequestHandler<UpdateUserCommand, bool> {
    private readonly DataContextWrite _context = context;

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
        try {
            var storedUser = await _context.Users.FindAsync(request.Id);
            ArgumentNullException.ThrowIfNull(storedUser, nameof(storedUser));

            storedUser.Name = request.Name;
            storedUser.UpdatedDate = DateTime.UtcNow;
            storedUser.DateOfBirth = request.DateOfBirth;
            storedUser.Rank = request.Rank;

            await _context.SaveChangesAsync();

            return true;
        } catch (Exception) {
            return false;
        }
    }
}
