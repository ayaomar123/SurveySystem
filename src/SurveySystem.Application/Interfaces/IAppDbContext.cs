using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entites;
using SurveySystem.Domain.Entites.Questions;
using SurveySystem.Domain.Entites.Surveys;
using SurveySystem.Domain.Entites.Surveys.Responses;

namespace SurveySystem.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Question> Questions { get; }
        public DbSet<QuestionChoice> QuestionChoices { get; }
        public DbSet<SliderConfig> SliderConfigs { get; }
        public DbSet<StarConfig> StarConfigs { get; }
        public DbSet<Survey> Surveys { get; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; }
        DbSet<SurveyResponse> SurveyResponses { get; }
        DbSet<SurveyAnswer> SurveyAnswers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
