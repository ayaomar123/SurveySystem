using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Infrastructure.Persistence.Configurations
{
    public class StarConfigConfiguration : IEntityTypeConfiguration<StarConfig>
    {
        public void Configure(EntityTypeBuilder<StarConfig> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.MaxStar).IsRequired();

            builder.Property(s => s.QuestionId).IsRequired();
        }
    }
}
