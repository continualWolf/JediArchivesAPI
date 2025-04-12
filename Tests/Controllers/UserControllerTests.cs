using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>> {
    private HttpClient _client;

    public UserControllerTests(WebApplicationFactory<Program> factory) {
        _client = factory
            .WithWebHostBuilder(builder => {
                builder.UseContentRoot(Directory.GetCurrentDirectory());
            })
            .CreateClient();
    }

    [Fact]
    public async Task Login_Should_Return_401_When_Invalid_Credentials() {
        var request = new { Id = 1, Password = "invalid-password" };

        var response = await _client.PostAsJsonAsync("/api/users/login", request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}