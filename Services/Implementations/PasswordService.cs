using JediArchives.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JediArchives.Services.Implementations;
public class PasswordService : IPasswordService {
    private readonly PasswordHasher<object> _hasher = new();

    public string HashPassword(string plainPassword) {
        return _hasher.HashPassword(null, plainPassword);
    }

    public bool VerifyPassword(string hashedPassword, string attemptedPassword) {
        var result = _hasher.VerifyHashedPassword(null, hashedPassword, attemptedPassword);
        return result == PasswordVerificationResult.Success;
    }
}
