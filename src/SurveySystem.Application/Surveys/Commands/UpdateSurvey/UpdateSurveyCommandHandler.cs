using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites.Surveys;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurvey
{
    public sealed class UpdateSurveyCommandHandler(IAppDbContext context, ICurrentUser user)
        : IRequestHandler<UpdateSurveyCommand, Unit>
    {

        public async Task<Unit> Handle(UpdateSurveyCommand request, CancellationToken ct)
        {
            var userId = user.UserId ?? throw new Exception("User not authenticated");

            var survey = await context.Surveys
                .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            var existingQuestions = await context.SurveyQuestions
                .Where(x => x.SurveyId == request.Id)
                .ToListAsync(ct);

            context.SurveyQuestions.RemoveRange(existingQuestions);
            survey.SurveyQuestions.Clear();

            survey.Update(
                request.Title,
                request.Description,
                request.StartDate,
                request.EndDate,
                userId
            );

            survey.UpdateStatus(request.Status, userId);

            foreach (var q in request.Questions)
            {
                //survey.AddQuestion(q.QuestionId, q.Order);
                SurveyQuestion.CreateSurveyQuestion(request.Id, q.QuestionId, q.Order);
            }

            await context.SaveChangesAsync(ct);

            return Unit.Value;

        }
    }
}
