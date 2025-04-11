using JediArchives.Application.Users.Models;
using JediArchives.DataStorage;
using MediatR;

namespace JediArchives.Application.Users.Queries;
public class GetUserByIdHandler(DataContextRead context) : IRequestHandler<GetUserByIdQuery, UserResponse?> {
    private readonly DataContextRead _context = context;

    public async Task<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);

        if (user is null) {
            return null;
        }

        return UserResponse.FromEF(user);
    }
}
