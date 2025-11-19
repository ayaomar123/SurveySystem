using MediatR;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var req = command.Request;

            var user = User.Create(
                req.Name,
                req.Email,
                passwordHasher.Hash(req.PasswordHash),
                req.Role);

            context.Users.Add(user);

            await context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
