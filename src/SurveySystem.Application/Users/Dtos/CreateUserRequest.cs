using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Dtos
{
    public class CreateUserRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
