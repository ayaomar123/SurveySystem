namespace SurveySystem.Application.Questionns.Dtos
{
    public sealed record QuestionResponseDto(
        Guid Id,
        string Title,
        string? Description,
        string QuestionType,
        bool IsRequired
    );
}
