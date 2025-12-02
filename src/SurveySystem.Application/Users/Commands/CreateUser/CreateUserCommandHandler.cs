using MediatR;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler(IAppDbContext context)
        : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken ct)
        {
            var user = User.Create(
                request.Name,
                request.Email,
                request.PasswordHash,
                request.Role
            );

            context.Users.Add(user);
            await context.SaveChangesAsync(ct);

            return user.Id;
        }
    }
}
