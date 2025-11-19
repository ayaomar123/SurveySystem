using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Users.Dtos
{
    public sealed class UsersResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public UserRole Role { get; set; }


    }
}
