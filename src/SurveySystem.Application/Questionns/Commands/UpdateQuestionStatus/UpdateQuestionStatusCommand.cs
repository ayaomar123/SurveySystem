using MediatR;

namespace SurveySystem.Application.Questionns.Commands.UpdateQuestionStatus
{
    public sealed record UpdateQuestionStatusCommand(Guid Id)
        : IRequest<bool>;
}
