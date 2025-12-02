using MediatR;
using SurveySystem.Application.Surveys.Responses.Queries.GetSurveyAnalytics.Dtos;

namespace SurveySystem.Application.Surveys.Responses.Queries.GetSurveyResponse
{
    public sealed record GetSurveyAnalyticsQuery(Guid SurveyId) : IRequest<GetSurveyAnalyticsDto>;
}
