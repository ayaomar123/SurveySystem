using MediatR;
using SurveySystem.Application.Questionns.Commands.UpdateQuestion.Dtos;
using SurveySystem.Application.Questionns.Dtos;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestion
{
    public sealed record UpdateQuestionCommand(
        UpdateQuestionDto Request) : IRequest<QuestionResponseDto>;
}
