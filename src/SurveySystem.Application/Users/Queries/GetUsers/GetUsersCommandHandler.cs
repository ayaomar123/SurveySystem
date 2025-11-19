using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Users.Dtos;

namespace SurveySystem.Application.Users.Queries.GetUsers
{
    public class GetUsersCommandHandler(IAppDbContext context) : IRequestHandler<GetUsersCommand, List<UsersResponse>>
    {
        public async Task<List<UsersResponse>> Handle(GetUsersCommand request, CancellationToken ct)
        {
            var users = await context.Users
                .Select(user => new UsersResponse
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role
                })
                .ToListAsync(ct);

            return users;
        }
    }
}
