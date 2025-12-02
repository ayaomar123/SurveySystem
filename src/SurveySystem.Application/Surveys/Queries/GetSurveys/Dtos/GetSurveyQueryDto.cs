using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys.Dtos
{
    public sealed record GetSurveyQueryDto(
        string? Title = null,
        SurveyStatus? Status = null
        );

}
