using MediatR;
using Microsoft.Extensions.Logging;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(
        IAppDbContext context,
        IPasswordHasher passwordHasher,
        ILogger<User> logger) : IRequestHandler<CreateUserCommand, Guid>
    {
        public async Task<Guid> Handle(
            CreateUserCommand command, CancellationToken cancellationToken)
        {
            var req = command.Request;

            User user = User.Create(
                req.Name,
                req.Email,
                passwordHasher.Hash(req.PasswordHash),
                req.Role);

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("User with: {name} and {email} created successfully!", user.Name, user.Email);
            return user.Id;
        }
    }
}
