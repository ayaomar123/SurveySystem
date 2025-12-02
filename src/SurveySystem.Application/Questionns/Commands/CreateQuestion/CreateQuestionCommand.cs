using MediatR;
using SurveySystem.Application.Questionns.Commands.CreateQuestion.Dtos;

namespace SurveySystem.Application.Questionns.Commands.CreateQuestion
{
    public sealed record CreateQuestionCommand(CreateQuestionDto Request) : IRequest<Guid>;
}
