using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Surveys.Queries.GetSurveys.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys
{
    public class GetSurveyHandler(IAppDbContext context)
        : IRequestHandler<GetSurveyQuery, List<GetSurveysResponseDto>>
    {
        public async Task<List<GetSurveysResponseDto>> Handle(GetSurveyQuery request, CancellationToken ct)
        {
            var query = context.Surveys
                .Include(s => s.SurveyQuestions)
                .Include(s => s.SurveyResponses)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Request.Title))
            {
                query = query.Where(s => s.Title.Contains(request.Request.Title));
            }

            if (request.Request.Status.HasValue)
            {
                query = query.Where(s => s.Status == request.Request.Status.Value);
            }

            var result = await query
                .Select(survey => new GetSurveysResponseDto(
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
                        .Select(q => new GetSurveyQuestionDto(
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
