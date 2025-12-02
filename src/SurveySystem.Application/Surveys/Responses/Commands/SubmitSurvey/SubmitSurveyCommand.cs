using MediatR;
using SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey.Dtos;

namespace SurveySystem.Application.Surveys.Responses.Commands.SubmitSurvey
{
    public sealed record SubmitSurveyCommand(SubmitSurveyCommandDto Request) : IRequest<Guid>;
}
