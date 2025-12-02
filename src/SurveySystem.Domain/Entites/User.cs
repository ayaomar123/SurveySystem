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

        private User()
        { 
        }

        private User(string name, string email, string passwordHash, UserRole role)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public static User Create(string name, string email, string passwordHash, UserRole userRole)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name required");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("email required");
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("password required");
            }

            return new User(name, email, passwordHash, userRole);
        }


        public void Update(string name, string email, string passwordHash, UserRole userRole)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name required");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("email required");
            }

            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("password required");
            }

            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = userRole;
        }

    }
}
