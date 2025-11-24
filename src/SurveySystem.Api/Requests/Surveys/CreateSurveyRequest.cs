using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Api.Requests.Surveys
{
    public sealed record CreateSurveyRequest(
        string Title,
        string? Description,
        SurveyStatus Status,
        DateTime? StartDate,
        DateTime? EndDate,
        List<SurveyQuestionRequest> Questions
    );
}
