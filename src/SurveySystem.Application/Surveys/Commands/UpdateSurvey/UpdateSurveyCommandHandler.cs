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
                .Include(s => s.SurveyQuestions)
                .FirstOrDefaultAsync(s => s.Id == request.Request.Id, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            survey.Update(
                request.Request.Title,
                request.Request.Description,
                request.Request.StartDate,
                request.Request.EndDate,
                userId
            );

            survey.UpdateStatus(request.Request.Status, userId);

            var existingQuestions = survey.SurveyQuestions.ToList();

            foreach (var existing in existingQuestions)
            {
                if (!request.Request.Questions.Any(q => q.QuestionId == existing.QuestionId))
                    context.SurveyQuestions.Remove(existing);
            }

            foreach (var q in request.Request.Questions)
            {
                var existing = existingQuestions.FirstOrDefault(x => x.QuestionId == q.QuestionId);

                if (existing is not null)
                {
                    if (existing.Order != q.Order)
                        existing.UpdateOrder(q.Order);
                }
                else
                {
                    survey.SurveyQuestions.Add(new SurveyQuestion(survey.Id, q.QuestionId, q.Order));
                }
            }

            await context.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
