using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Surveys.Responses;

namespace SurveySystem.Infrastructure.Configurations
{
    public class SurveyAnswerConfiguration : IEntityTypeConfiguration<SurveyAnswer>
    {
        public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.SurveyResponseId)
                   .IsRequired();

            builder.Property(a => a.QuestionId)
                   .IsRequired();

            builder.Property(x => x.Value)
                    .HasMaxLength(2000);

            builder.Property(x => x.SelectedChoiceId);

            builder.Property(x => x.SelectedChoicesIds)
                .HasConversion(
                    list => list == null ? null : string.Join(",", list),
                    str => string.IsNullOrWhiteSpace(str)
                            ? new List<Guid>()
                            : str.Split(new[] { ',' }, StringSplitOptions.None)
                                 .Select(Guid.Parse)
                                 .ToList()
                );


            builder.HasOne(a => a.SurveyResponse)
                   .WithMany(r => r.Answers)
                   .HasForeignKey(a => a.SurveyResponseId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Question)
                   .WithMany()
                   .HasForeignKey(x => x.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
