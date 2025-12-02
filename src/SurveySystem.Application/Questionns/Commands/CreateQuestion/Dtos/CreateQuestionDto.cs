using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Commands.CreateQuestion.Dtos
{
    public sealed record CreateQuestionDto
        (string Title,
        string? Description,
        QuestionTypeDto QuestionType,
        bool IsRequired,
        bool Status,
        List<QuestionChoiceDto>? Choices,
        SliderConfigDto? Config,
        StarConfigDto? StarConfig);
}
