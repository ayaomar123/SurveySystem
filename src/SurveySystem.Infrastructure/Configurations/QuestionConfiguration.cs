using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Infrastructure.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title).HasMaxLength(300).IsRequired();

            builder.Property(q => q.Description).HasMaxLength(1000);

            builder.Property(q => q.QuestionType).IsRequired();

            builder.Property(q => q.IsRequired).IsRequired();

            builder.Property(q => q.Status).HasDefaultValue(true);

            builder.Property(q => q.CreatedAt).IsRequired();

            builder.HasMany(q => q.Choices).WithOne().HasForeignKey(c => c.QuestionId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.SliderConfig).WithOne().HasForeignKey<SliderConfig>(s => s.QuestionId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(q => q.StarConfig).WithOne().HasForeignKey<StarConfig>(s => s.QuestionId).OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.SurveyQuestions)
               .WithOne(sq => sq.Question)
               .HasForeignKey(sq => sq.QuestionId);

        }
    }
}
