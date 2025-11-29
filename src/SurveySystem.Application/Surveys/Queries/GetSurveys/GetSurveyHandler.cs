using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Surveys.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys
{
    public class GetSurveyHandler(IAppDbContext context)
        : IRequestHandler<GetSurveyQuery, List<SurveyDto>>
    {
        public async Task<List<SurveyDto>> Handle(GetSurveyQuery request, CancellationToken ct)
        {
            var surveys = await context.Surveys
                .Select(survey => new SurveyDto(
                    survey.Id,
                    survey.Title,
                    survey.Status,
                    survey.CreatedAt,
                    survey.LastModifiedDate,
                    survey.SurveyQuestions.Count,
                    0, // TODO: replace when Response table is added
                    survey.StartDate,
                    survey.EndDate,
                    survey.SurveyQuestions
                        .OrderBy(q => q.Order)
                        .Select(q => new SurveyQuestionDto(
                            q.QuestionId,
                            q.Question!.Title,
                            (int)q.Question!.QuestionType,
                            q.Order
                        )).ToList()
                ))
                .ToListAsync(ct);

            return surveys;
        }
    }
}
