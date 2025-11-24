using MediatR;
using SurveySystem.Application.Users.Dtos;

namespace SurveySystem.Application.Users.Queries.GetUsers
{
    public sealed record GetUsersQuery() : IRequest<List<UsersResponse>>;
}
