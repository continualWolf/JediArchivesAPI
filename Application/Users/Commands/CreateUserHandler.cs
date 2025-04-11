using JediArchives.Application.Users.Models;
using JediArchives.DataStorage;
using JediArchives.Services.Interfaces;
using MediatR;

namespace JediArchives.Application.Users.Commands;

public class CreateUserHandler(DataContextWrite context, IPasswordService passwordService) : IRequestHandler<CreateUserCommand, UserResponse> {
    private readonly DataContextWrite _context = context;
    private readonly IPasswordService _passwordService = passwordService;

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
        var hashedPassword = _passwordService.HashPassword(request.Password);

        var user = request.ToEF();

        user.HashedPassword = hashedPassword;

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return UserResponse.FromEF(user);
    }
}
