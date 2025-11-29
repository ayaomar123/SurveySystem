using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Surveys.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys
{
    public class GetSurveyHandler(IAppDbContext context)
        : IRequestHandler<GetSurveyQuery, List<SurveysResponseDto>>
    {
        public async Task<List<SurveysResponseDto>> Handle(GetSurveyQuery request, CancellationToken ct)
        {
            var surveys = await context.Surveys
                .Select(survey => new SurveysResponseDto(
                    survey.Id,
                    survey.Title,
                    survey.Status,
                    survey.CreatedAt,
                    survey.LastModifiedDate,
                    survey.SurveyQuestions.Count,
                    0,
                    survey.StartDate,
                    survey.EndDate,
                    survey.SurveyQuestions
                        .OrderBy(q => q.Order)
                        .Select(q => new SurveyQuestionDto(
                            q.QuestionId,
                            q.Question!.Title,
                            q.Order
                        )).ToList()
                ))
                .ToListAsync(ct);

            return surveys;
        }
    }
}
