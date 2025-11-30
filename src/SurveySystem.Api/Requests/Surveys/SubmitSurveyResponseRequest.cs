using SurveySystem.Application.Surveys.Responses.Dtos;

namespace SurveySystem.Api.Requests.Surveys
{
    public class SubmitSurveyResponseRequest
    {
        public List<AnswerRequest> Answers { get; set; } = new();
    }

    public class AnswerRequest
    {
        public Guid QuestionId { get; set; }
        public string? Value { get; set; }
        public Guid? SelectedChoiceId { get; set; }
        public List<Guid>? SelectedChoices { get; set; }
    }

}
