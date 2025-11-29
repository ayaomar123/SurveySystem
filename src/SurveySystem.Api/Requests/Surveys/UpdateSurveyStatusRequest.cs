using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Api.Requests.Surveys
{
    public sealed record UpdateSurveyStatusRequest(SurveyStatus Status, DateTime? StartDate,
        DateTime? EndDate);
}
