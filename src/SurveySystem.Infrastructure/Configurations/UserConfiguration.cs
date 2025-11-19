using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites;

namespace SurveySystem.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                     .IsRequired()
                     .HasMaxLength(200);

            builder.Property(u => u.PasswordHash)
                     .IsRequired();

            builder.Property(u => u.Role)
                        .IsRequired();
        }
    }
}
