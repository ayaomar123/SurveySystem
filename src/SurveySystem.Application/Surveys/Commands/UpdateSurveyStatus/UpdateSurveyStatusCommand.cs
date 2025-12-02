using MediatR;
using SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus.Dtos;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurveyStatus
{
    public sealed record UpdateSurveyStatusCommand(
       UpdateSurveyStatusDto Request
        ) : IRequest<Unit>;
}
