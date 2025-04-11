using JediArchives.Application.Users.Models;
using MediatR;

namespace JediArchives.Application.Users.Queries;
public class LoginQuery : IRequest<LoginResponse> {
    public int Id { get; set; }
    public string Password { get; set; }

    public LoginQuery(int id, string password) {
        Id = id;
        Password = password;
    }
}
