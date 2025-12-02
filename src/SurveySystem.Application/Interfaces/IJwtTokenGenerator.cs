namespace SurveySystem.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string name, string email, string role);
    }
}
