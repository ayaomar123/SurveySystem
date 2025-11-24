using MediatR;
using SurveySystem.Application.Users.Dtos.Login;

namespace SurveySystem.Application.Users.Commands.Login
{
    public sealed record LoginCommand(string Email, string Password): IRequest<LoginResponse>;
}
