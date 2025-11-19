using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Dtos.Login
{
    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
