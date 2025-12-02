using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites.Surveys.Responses;

namespace SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey
{
    public class SubmitSurveyCommandHandler(IAppDbContext context)
        : IRequestHandler<SubmitSurveyCommand, Guid>
    {
        public async Task<Guid> Handle(SubmitSurveyCommand request, CancellationToken ct)
        {
            var survey = await context.Surveys
                .Include(s => s.SurveyQuestions)
                .ThenInclude(sq => sq.Question)
                .FirstOrDefaultAsync(s => s.Id == request.Request.SurveyId, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            var surveyQuestionIds = survey.SurveyQuestions
                .Select(sq => sq.QuestionId)
                .ToHashSet();

            var response = new SurveyResponse(
                surveyId: request.Request.SurveyId,
                ipAddress: request.Request.IpAddress,
                userAgent: request.Request.UserAgent
            );

            foreach (var ans in request.Request.Answers)
            {
                SurveyAnswer answer;

                if (ans.SelectedChoiceId.HasValue)
                {
                    answer = SurveyAnswer.CreateSingleChoice(
                        ans.QuestionId,
                        ans.SelectedChoiceId.Value
                    );
                }
                else if (ans.SelectedChoicesIds != null && ans.SelectedChoicesIds.Count > 0)
                {
                    answer = SurveyAnswer.CreateMultipleChoices(
                        ans.QuestionId,
                        ans.SelectedChoicesIds
                    );
                }
                else if (!string.IsNullOrWhiteSpace(ans.Value))
                {
                    answer = SurveyAnswer.CreateValue(
                        ans.QuestionId,
                        ans.Value
                    );
                }
                else
                {
                    throw new Exception($"No valid answer provided for question '{ans.QuestionId}'.");
                }

                response.AddAnswer(answer);
            }

            context.SurveyResponses.Add(response);
            await context.SaveChangesAsync(ct);

            return response.Id;
        }
    }
}
