using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SurveySystem.Domain.Entites
{
    public enum UserRole
    {
        Admin,
        Designer
    }
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; } = UserRole.Designer;

        public User()
        {
        }

        public User(string name, string email, string passwordHash, UserRole role)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public static User Create(string name, string email, string passwordHash, UserRole userRole)
        {
            return new User(name, email, passwordHash, userRole);
        }
    
}
}
