using MediatR;
using SurveySystem.Application.Surveys.Responses.Dtos;

namespace SurveySystem.Application.Surveys.Responses.Queries.GetSurveyResponse
{
    public sealed record GetSurveyAnalyticsQuery(Guid SurveyId) : IRequest<SurveyAnalyticsDto>;
}
