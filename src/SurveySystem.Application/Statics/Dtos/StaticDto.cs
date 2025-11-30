namespace SurveySystem.Application.Statics.Dtos
{
    public sealed record StaticDto(
        int TotalResponses,
        int TotalSurveys,
        int ActiveSurveys,
        int TotalQuestions,
        List<ResponseDto> Surveys
        );

    public sealed record ResponseDto(
        Guid Id,
        String Title
        );
}
