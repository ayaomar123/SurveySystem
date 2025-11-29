namespace SurveySystem.Application.Surveys.Dtos
{
    public sealed record SurveyQuestionDto(
        Guid QuestionId,
        string Title,
        int Order 
    );
}