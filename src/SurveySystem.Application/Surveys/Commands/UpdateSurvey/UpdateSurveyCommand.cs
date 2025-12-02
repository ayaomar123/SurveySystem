using MediatR;
using SurveySystem.Application.Surveys.Commands.UpdateSurvey.Dtos;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurvey
{
    public sealed record UpdateSurveyCommand(
       UpdateSurveyDto Request
    ) : IRequest<Unit>;
}
