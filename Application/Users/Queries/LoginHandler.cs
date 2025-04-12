using JediArchives.Application.Users.Models;
using JediArchives.DataStorage;
using JediArchives.Services.Interfaces;
using MediatR;

namespace JediArchives.Application.Users.Queries;
public class LoginHandler(DataContextRead context, IPasswordService passwordService, IJwtService jwtService) : IRequestHandler<LoginQuery, LoginResponse> {
    private readonly DataContextRead _context = context;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<LoginResponse?> Handle(LoginQuery request, CancellationToken cancellationToken) {
        if (string.IsNullOrEmpty(request.Password)) {
            throw new UnauthorizedAccessException();
        }

        var user = await _context.Users.FindAsync(request.Id);

        if (user is null) {
            throw new UnauthorizedAccessException();
        }

        bool valid = _passwordService.VerifyPassword(user.HashedPassword, request.Password);

        if (!valid) {
            throw new UnauthorizedAccessException();
        }

        var token = _jwtService.GenerateToken(user.Id, user.Name, user.Rank);

        return LoginResponse.FromEF(token, user);
    }
}
