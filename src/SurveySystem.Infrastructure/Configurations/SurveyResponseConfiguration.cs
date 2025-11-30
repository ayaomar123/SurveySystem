using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Surveys.Responses;

namespace SurveySystem.Infrastructure.Configurations
{
    public class SurveyResponseConfiguration : IEntityTypeConfiguration<SurveyResponse>
    {
        public void Configure(EntityTypeBuilder<SurveyResponse> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.SurveyId)
                   .IsRequired();


            builder.Property(r => r.SubmittedAt)
                   .IsRequired();

            builder.Property(r => r.IpAddress)
                   .HasMaxLength(50);

            builder.Property(r => r.UserAgent)
                   .HasMaxLength(500);

            builder.HasMany(r => r.Answers)
                   .WithOne(a => a.SurveyResponse)
                   .HasForeignKey(a => a.SurveyResponseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
