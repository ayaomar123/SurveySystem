using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Infrastructure.Persistence.Configurations
{
    public class SliderConfigConfiguration : IEntityTypeConfiguration<SliderConfig>
    {
        public void Configure(EntityTypeBuilder<SliderConfig> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Min).IsRequired();

            builder.Property(s => s.Max).IsRequired();

            builder.Property(s => s.Step).IsRequired();

            builder.Property(s => s.UnitLabel).HasMaxLength(50);

            builder.Property(s => s.QuestionId).IsRequired();
        }
    }
}
