using MediatR;
using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid Id,
        string Name,
        string Email,
        string PasswordHash,
        UserRole Role

        ) : IRequest<Unit>;
}
