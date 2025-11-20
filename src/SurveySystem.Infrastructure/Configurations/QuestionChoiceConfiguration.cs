using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Infrastructure.Persistence.Configurations
{
    public class QuestionChoiceConfiguration : IEntityTypeConfiguration<QuestionChoice>
    {
        public void Configure(EntityTypeBuilder<QuestionChoice> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Text).HasMaxLength(200).IsRequired();

            builder.Property(c => c.Order).IsRequired();

            builder.Property(c => c.QuestionId).IsRequired();
        }
    }
}
