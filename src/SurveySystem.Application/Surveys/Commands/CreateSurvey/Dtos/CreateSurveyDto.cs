using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Commands.CreateSurvey.Dtos
{
    public sealed record CreateSurveyDto(
         string Title,
        string? Description,
        SurveyStatus Status,
        DateTime? StartDate,
        DateTime? EndDate,
        List<SurveyQuestionItem> Questions
        );

    public sealed record SurveyQuestionItem(Guid QuestionId, int Order);

}
