using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Dtos.Details
{
    public sealed record SurveyDetailsDto(
        Guid Id,
        string Title,
        string Description,
        SurveyStatus Status,
        DateTime? StartDate,
        DateTime? EndDate,
        DateTime CreatedAt,
        DateTime? LastModifiedDate,
        Guid CreatedBy,
        string CreatedByName,
        Guid? lastModifiedBy,
        string? lastModifiedByName,
        List<SurveyQuestionDetailsDto> Questions
        );

}
