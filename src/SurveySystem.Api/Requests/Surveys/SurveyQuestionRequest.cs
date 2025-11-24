namespace SurveySystem.Api.Requests.Surveys
{
    public sealed record SurveyQuestionRequest(Guid QuestionId, int Order);
}
