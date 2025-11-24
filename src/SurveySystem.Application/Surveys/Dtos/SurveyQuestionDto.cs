namespace SurveySystem.Application.Surveys.Dtos
{
    public sealed record SurveyQuestionDto(
        Guid QuestionId,
        string Title,
        string QuestionType,
        int Order
    );
}