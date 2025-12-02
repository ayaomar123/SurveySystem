using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus
{
    public sealed class UpdateSurveyStatusCommandHandler
        (IAppDbContext context, ICurrentUser user)
        : IRequestHandler<UpdateSurveyStatusCommand, Unit>
    {

        public async Task<Unit> Handle(UpdateSurveyStatusCommand request, CancellationToken ct)
        {
            var userId = user.UserId ?? throw new Exception("User not authenticated");

            var survey = await context.Surveys
                .FirstOrDefaultAsync(s => s.Id == request.Request.Id, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            survey.UpdateStatus(
                request.Request.Status,
                userId,
                request.Request.StartDate,
                request.Request.EndDate);

            await context.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
