using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys.Dtos
{
    public sealed record GetSurveysResponseDto(
        Guid Id,
        string Title,
        SurveyStatus Status,
        DateTime CreatedAt,
        DateTime? LastModifiedDate,
        int QuestionsCount,
        int ResponsesCount,
        DateTime? StartDate,
        DateTime? EndDate,
        List<GetSurveyQuestionDto> Questions
        );

}
