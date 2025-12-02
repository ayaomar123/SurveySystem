namespace SurveySystem.Application.Surveys.Queries.GetSurveys.Dtos
{
    public sealed record GetSurveyQuestionDto(
        Guid? QuestionId,
        string Title,
        int Order 
    );
}