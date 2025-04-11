using JediArchives.Application.Users.Models;
using MediatR;

namespace JediArchives.Application.Users.Queries;
public class GetUserByIdQuery : IRequest<UserResponse> {
    public int Id { get; set; }

    public GetUserByIdQuery(int id) {
        Id = id;
    }
}
