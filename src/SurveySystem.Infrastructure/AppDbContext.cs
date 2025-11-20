using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites;
using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Infrastructure
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<QuestionChoice> QuestionChoices => Set<QuestionChoice>();
        public DbSet<SliderConfig> SliderConfigs => Set<SliderConfig>();
        public DbSet<StarConfig> StarConfigs => Set<StarConfig>();

        public override Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
