using Microsoft.AspNetCore.Mvc;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Api.Requests.Surveys
{
    public sealed record GetSurveyQueryRequest(
        string? title,
        SurveyStatus? status,
        bool? hasResponses = null
        );
}
