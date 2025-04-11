using JediArchives.Services.Implementations;
using Xunit;

namespace JediArchives.Tests.Services;
public class PasswordServiceTests {
    private readonly PasswordService _service = new();

    [Fact]
    public void HashPassword_Should_Return_NonEmpty_String() {
        // Arrange
        var plainPassword = "ThisIsTheWay";

        // Act
        var hash = _service.HashPassword(plainPassword);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(hash));
        Assert.NotEqual(plainPassword, hash); // it should be hashed
    }

    [Fact]
    public void VerifyPassword_Should_Return_True_For_Valid_Match() {
        // Arrange
        var password = "Grogu123";
        var hash = _service.HashPassword(password);

        // Act
        var result = _service.VerifyPassword(hash, password);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_Should_Return_False_For_Invalid_Match() {
        // Arrange
        var hash = _service.HashPassword("correct-password");

        // Act
        var result = _service.VerifyPassword(hash, "incorrect-password");

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void VerifyPassword_Should_Return_False_If_Hash_Is_Tampered() {
        // Arrange
        var password = "ValidOne";
        var hash = _service.HashPassword(password);

        // Modify a character in the middle but keep it Base64-valid
        var hashChars = hash.ToCharArray();
        hashChars[10] = hashChars[10] == 'A' ? 'B' : 'A'; // flip one char
        var tamperedHash = new string(hashChars);

        // Act
        var result = _service.VerifyPassword(tamperedHash, password);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HashPassword_Should_Be_Different_Each_Time() {
        // Arrange
        var password = "CloneTrooper";

        // Act
        var hash1 = _service.HashPassword(password);
        var hash2 = _service.HashPassword(password);

        // Assert
        Assert.NotEqual(hash1, hash2); // hashes include random salt
    }
}
