using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Surveys;

namespace SurveySystem.Infrastructure.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Description)
                .HasMaxLength(1000);

            builder.Property(s => s.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(s => s.StartDate)
                .IsRequired(false);

            builder.Property(s => s.EndDate)
                .IsRequired(false);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.CreatedBy)
                .IsRequired();

            builder.Property(s => s.LastModifiedDate)
                .IsRequired(false);

            builder.Property(s => s.LastModifiedBy)
                .IsRequired(false);

            builder.HasOne(s => s.CreatedByUser)
                   .WithMany()
                   .HasForeignKey(s => s.CreatedBy)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(s => s.LastModifiedByUser)
                   .WithMany()
                   .HasForeignKey(s => s.LastModifiedBy)
                   .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
