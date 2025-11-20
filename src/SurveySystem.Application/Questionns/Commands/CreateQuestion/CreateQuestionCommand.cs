using MediatR;
using SurveySystem.Application.Questionns.Dtos;
namespace SurveySystem.Application.Questionns.Commands.CreateQuestion
{
    public sealed record CreateQuestionCommand(CreateQuestionRequest Request) : IRequest<Guid>;
}
