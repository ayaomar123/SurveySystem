namespace SurveySystem.Application.Surveys.Dtos
{
    public sealed record SurveyQuestionDto(
        Guid QuestionId,
        string Title,
        int QuestionType,
        int Order
    );
}