using MediatR;
using SurveySystem.Application.Users.Dtos;
using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        string Name,
        string Email,
        string PasswordHash,
        UserRole Role) : IRequest<Guid>;
}
