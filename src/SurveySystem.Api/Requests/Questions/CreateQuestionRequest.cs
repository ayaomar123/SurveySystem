using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Api.Requests.Questions
{
    public sealed record CreateQuestionRequest(
        string Title,
        string? Description,
        QuestionTypeDto QuestionType,
        bool IsRequired,
        bool Status,
        List<QuestionChoiceDto>? Choices,
        SliderConfigDto? Config,
        StarConfigDto? StarConfig);
}
