using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus.Dtos
{
    public sealed record UpdateSurveyStatusDto(
        Guid Id,
        SurveyStatus Status,
        DateTime? StartDate,
        DateTime? EndDate);
}
