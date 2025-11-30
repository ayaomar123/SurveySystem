using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Domain.Entites.Surveys.Responses;

namespace SurveySystem.Application.Surveys.Responses.Commands.SubmitSurveyResponse
{
    public class SubmitSurveyResponseCommandHandler
        : IRequestHandler<SubmitSurveyResponseCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public SubmitSurveyResponseCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(SubmitSurveyResponseCommand request, CancellationToken ct)
        {
            var survey = await _context.Surveys
                .Include(s => s.SurveyQuestions)
                .ThenInclude(sq => sq.Question)
                .FirstOrDefaultAsync(s => s.Id == request.SurveyId, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            var surveyQuestionIds = survey.SurveyQuestions
                .Select(sq => sq.QuestionId)
                .ToHashSet();

            foreach (var ans in request.Answers)
            {
                if (!surveyQuestionIds.Contains(ans.QuestionId))
                    throw new Exception($"Question '{ans.QuestionId}' does not belong to this survey.");
            }

            var response = new SurveyResponse(
                surveyId: request.SurveyId,
                ipAddress: request.IpAddress,
                userAgent: request.UserAgent
            );

            foreach (var ans in request.Answers)
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

            _context.SurveyResponses.Add(response);
            await _context.SaveChangesAsync(ct);

            return response.Id;
        }
    }
}
