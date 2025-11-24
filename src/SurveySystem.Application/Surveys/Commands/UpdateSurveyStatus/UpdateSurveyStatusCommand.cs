using MediatR;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus
{
    public sealed record UpdateSurveyStatusCommand(Guid Id,
        SurveyStatus Status) : IRequest<Unit>;
}
