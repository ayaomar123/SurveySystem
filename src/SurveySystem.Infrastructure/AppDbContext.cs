using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites;
using SurveySystem.Domain.Entites.Questions;
using SurveySystem.Domain.Entites.Surveys;
using SurveySystem.Domain.Entites.Surveys.Responses;

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
        public DbSet<Survey> Surveys => Set<Survey>();
        public DbSet<SurveyQuestion> SurveyQuestions => Set<SurveyQuestion>();
        public DbSet<SurveyResponse> SurveyResponses => Set<SurveyResponse>();
        public DbSet<SurveyAnswer> SurveyAnswers => Set<SurveyAnswer>();

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }

        public async Task SeedAsync()
        {
            await DbSeeder.SeedUserAsync(this);
            await DbSeeder.SeedQuestionsAsync(this);
            await DbSeeder.SeedSurveysAsync(this);
        }

    }
}
