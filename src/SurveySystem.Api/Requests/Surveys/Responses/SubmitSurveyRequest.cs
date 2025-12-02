namespace SurveySystem.Api.Requests.Surveys.Responses
{
    public class SubmitSurveyRequest
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
