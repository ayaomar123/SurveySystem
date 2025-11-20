using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entites;
using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Question> Questions { get; }
        public DbSet<QuestionChoice> QuestionChoices { get; }
        public DbSet<SliderConfig> SliderConfigs { get; }
        public DbSet<StarConfig> StarConfigs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
