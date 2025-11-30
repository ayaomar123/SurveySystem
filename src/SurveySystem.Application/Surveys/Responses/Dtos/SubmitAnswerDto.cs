namespace SurveySystem.Application.Surveys.Responses.Dtos
{
    public class SubmitAnswerDto
    {
        public Guid QuestionId { get; set; }

        public string? Value { get; set; }             
        public Guid? SelectedChoiceId { get; set; }     
        public List<Guid>? SelectedChoicesIds { get; set; }
    }
}
