using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Application.Questionns.Dtos
{
    public sealed record QuestionResponseDto(
        Guid Id,
        string Title,
        string? Description,
        int QuestionType,
        bool IsRequired,
        bool Status,
        DateTime CreatedAt,
        List<QuestionChoice>? Choices,
        SliderConfig? SliderConfig,
        StarConfig? StarConfig
    );
}
