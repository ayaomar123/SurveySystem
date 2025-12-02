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
            var query = context.Surveys
                .Include(s => s.SurveyQuestions)
                .Include(s => s.SurveyResponses)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(s => s.Title.Contains(request.Title));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(s => s.Status == request.Status.Value);
            }

            if (request.HasResponses.HasValue)
            {
                if (request.HasResponses.Value)
                {
                    query = query.Where(s => s.SurveyResponses.Any());
                }
                else
                {
                    query = query.Where(s => !s.SurveyResponses.Any());
                }
            }

            var result = await query
                .Select(survey => new SurveysResponseDto(
                    survey.Id,
                    survey.Title,
                    survey.Status,
                    survey.CreatedAt,
                    survey.LastModifiedDate,
                    survey.SurveyQuestions.Count,
                    survey.SurveyResponses.Count,
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

            return result;
        }
    }
}
