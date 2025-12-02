using SurveySystem.Application.Surveys.Commands.CreateSurvey.Dtos;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurvey.Dtos
{
    public sealed record UpdateSurveyDto(
         Guid Id,
        string Title,
        string? Description,
        SurveyStatus Status,
        DateTime? StartDate,
        DateTime? EndDate,
        List<SurveyQuestionItem> Questions);
}
