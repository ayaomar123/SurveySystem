using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;

namespace SurveySystem.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IAppDbContext context)
        : IRequestHandler<UpdateUserCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateUserCommand command, CancellationToken ct)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Id == command.Id, ct);

            if (user is null)
                throw new Exception("User not found");

            user.Update(
                command.Name,
                command.Email,
                command.PasswordHash,
                command.Role
            );

            await context.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
