using MediatR;
using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestion
{
    public sealed record UpdateQuestionCommand(Guid Id,
        string Title,
        string? Description,
        QuestionTypeDto QuestionType,
        bool IsRequired,
        bool Status,
        List<QuestionChoiceDto>? Choices,
        SliderConfigDto? SliderConfig,
        StarConfigDto? StarConfig) : IRequest<QuestionResponseDto>;
}
