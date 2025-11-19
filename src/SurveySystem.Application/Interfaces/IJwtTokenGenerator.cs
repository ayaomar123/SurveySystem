using SurveySystem.Application.Users.Dtos.Login;

namespace SurveySystem.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
