using MediatR;
using SurveySystem.Application.Surveys.Responses.Dtos;

namespace SurveySystem.Application.Surveys.Responses.Commands.SubmitSurveyResponse
{
    public class SubmitSurveyResponseCommand : IRequest<Guid>
    {
        public Guid SurveyId { get; set; }

        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public List<SubmitAnswerDto> Answers { get; set; } = new();
    }
}
