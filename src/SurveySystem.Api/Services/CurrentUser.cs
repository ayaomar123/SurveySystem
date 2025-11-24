using System.Security.Claims;
using SurveySystem.Application.Interfaces;

namespace SurveySystem.Api.Services
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
    {
        public Guid? UserId
        {
            get
            {
                var userId = httpContextAccessor
                    .HttpContext?
                    .User?
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId is null)
                    return null;

                return Guid.Parse(userId);
            }
        }
    }
}
