using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Dtos
{
    public sealed record SurveyDto(
        Guid Id,
        string Title,
        SurveyStatus Status,
        DateTime CreatedAt,
        DateTime? LastModifiedDate,
        int QuestionsCount,
        int ResponsesCount,
        DateTime? StartDate,
        DateTime? EndDate,
        List<SurveyQuestionDto> Questions
        );

    public sealed record SurveyQuestionsDto(
        string Title,
        int Order
    );
}
