namespace JediArchives.Services.Interfaces;
public interface IPasswordService {
    string HashPassword(string plainPassword);

    bool VerifyPassword(string hashedPassword, string attemptedPassword);
}
