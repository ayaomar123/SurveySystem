using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entites;

namespace SurveySystem.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
