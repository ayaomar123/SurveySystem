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
                .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            foreach (var sq in survey.SurveyQuestions.ToList())
            {
                context.SurveyQuestions.Remove(sq);
            }

            survey.SurveyQuestions.Clear();

            foreach (var q in request.Questions)
            {
                survey.SurveyQuestions.Add(
                    new SurveyQuestion(survey.Id, q.QuestionId, q.Order)
                );
            }

            survey.Update(
                request.Title,
                request.Description,
                request.StartDate,
                request.EndDate,
                userId
            );

            survey.UpdateStatus(request.Status, userId);

            /*foreach (var q in request.Questions)
            {
                SurveyQuestion.CreateSurveyQuestion(request.Id, q.QuestionId, q.Order);
            }*/

            
            await context.SaveChangesAsync(ct);

            return Unit.Value;

        }
    }
}
