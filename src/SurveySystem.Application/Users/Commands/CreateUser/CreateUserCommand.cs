using MediatR;
using SurveySystem.Application.Users.Dtos;

namespace SurveySystem.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(CreateUserRequest Request) : IRequest<Guid>;
}
