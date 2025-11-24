using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurvey
{
    public sealed class UpdateSurveyCommandHandler(IAppDbContext context, ICurrentUser user)
        : IRequestHandler<UpdateSurveyCommand, Unit>
    {

        public async Task<Unit> Handle(UpdateSurveyCommand request, CancellationToken ct)
        {
            var userId = user.UserId ?? throw new Exception("User not authenticated");

            var survey = await context.Surveys
                .Include(s => s.SurveyQuestions)
                .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            // Delete existing questions from DB
            var existingQuestions = await context.SurveyQuestions
                .Where(x => x.SurveyId == request.Id)
                .ToListAsync(ct);

            context.SurveyQuestions.RemoveRange(existingQuestions);
            survey.SurveyQuestions.Clear();

            // Update main survey fields
            survey.Update(
                request.Title,
                request.Description,
                request.StartDate,
                request.EndDate,
                userId
            );

            // Only if status changed
            survey.UpdateStatus(request.Status, userId);

            // Add new questions
            foreach (var q in request.Questions)
            {
                survey.AddQuestion(q.QuestionId, q.Order);
            }

            await context.SaveChangesAsync(ct);

            return Unit.Value;

        }
    }
}
