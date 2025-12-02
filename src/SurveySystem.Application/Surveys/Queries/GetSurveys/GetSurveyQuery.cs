using MediatR;
using SurveySystem.Application.Surveys.Dtos;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys
{
    public sealed record GetSurveyQuery(
        string? Title = null,
        SurveyStatus? Status = null,
        bool? HasResponses = null
        ) 
        : IRequest<List<SurveysResponseDto>>;
}
