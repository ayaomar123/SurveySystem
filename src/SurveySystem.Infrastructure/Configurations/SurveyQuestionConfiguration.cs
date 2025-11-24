using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Surveys;

namespace SurveySystem.Infrastructure.Persistence.Configurations
{
    public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
    {
        public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
        {
            builder.HasKey(sq => sq.Id);

            builder.Property(sq => sq.SurveyId)
                .IsRequired();

            builder.Property(sq => sq.QuestionId)
                .IsRequired();

            builder.Property(sq => sq.Order)
                .IsRequired();

            builder.HasOne<Survey>()
                   .WithMany(s => s.SurveyQuestions)
                   .HasForeignKey(sq => sq.SurveyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sq => sq.Question)
                   .WithMany()
                   .HasForeignKey(sq => sq.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
