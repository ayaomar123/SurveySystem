using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestion.Dtos
{
    public sealed record UpdateQuestionDto(
        Guid Id,
        string Title,
        string? Description,
        QuestionTypeDto QuestionType,
        bool IsRequired,
        bool Status,
        List<QuestionChoiceDto>? Choices,
        SliderConfigDto? SliderConfig,
        StarConfigDto? StarConfig);
}
