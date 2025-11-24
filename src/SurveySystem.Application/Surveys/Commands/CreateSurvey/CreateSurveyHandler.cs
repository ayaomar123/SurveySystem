using MediatR;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites.Surveys;

namespace SurveySystem.Application.Surveys.Commands.CreateSurvey
{
    public sealed class CreateSurveyHandler(IAppDbContext context, ICurrentUser user)
                : IRequestHandler<CreateSurveyCommand, Guid>
    {
        public async Task<Guid> Handle(CreateSurveyCommand request, CancellationToken ct)
        {
            var survey = Survey.Create(
                request.Title,
                request.Description,
                request.Status,
                request.StartDate,
                request.EndDate,
                user.UserId!.Value
            );

            foreach (var q in request.Questions)
            {
                survey.AddQuestion(q.QuestionId, q.Order);
            }

            await context.Surveys.AddAsync(survey, ct);
            await context.SaveChangesAsync(ct);

            return survey.Id;
        }
    }
}
