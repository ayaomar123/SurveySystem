namespace SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey.Dtos
{
    public sealed record SubmitSurveyCommandDto(
        Guid SurveyId,
        string? IpAddress,
        string? UserAgent,
        List<SubmitSurveyAnswerDto> Answers
        );
}
