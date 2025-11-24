using MediatR;
using SurveySystem.Application.Surveys.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys
{
    public sealed record GetSurveyQuery() : IRequest<List<SurveyDto>>;
}
