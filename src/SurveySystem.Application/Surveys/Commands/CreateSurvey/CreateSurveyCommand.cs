using MediatR;
using SurveySystem.Application.Surveys.Commands.CreateSurvey.Dtos;

namespace SurveySystem.Application.Surveys.Commands.CreateSurvey
{
    public sealed record CreateSurveyCommand(
       CreateSurveyDto Request
    ) : IRequest<Guid>;
}
