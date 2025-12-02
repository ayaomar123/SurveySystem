using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Users.Dtos.Login;

namespace SurveySystem.Application.Users.Commands.Login
{
    public class LoginCommandHandler
        (IAppDbContext context,
        IJwtTokenGenerator tokenGenerator,
        IPasswordHasher hasher) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email, ct);

            if (user == null)
                throw new Exception("User email not found");

            if (!hasher.Verify(request.Password, user.PasswordHash))
                throw new Exception("Error password");

            var token = tokenGenerator.GenerateToken(
                user.Id,
                user.Name,
                user.Email,
                user.Role.ToString()
            );

            return new LoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role,
                Token = token
            };
        }
    }
}
