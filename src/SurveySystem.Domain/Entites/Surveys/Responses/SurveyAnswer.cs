using SurveySystem.Domain.Entites.Questions;

namespace SurveySystem.Domain.Entites.Surveys.Responses
{
    public class SurveyAnswer
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid SurveyResponseId { get; private set; }
        public SurveyResponse SurveyResponse { get; private set; } = default!;

        public Guid QuestionId { get; private set; }
        public Question Question { get; private set; } = default!;
        public string? Value { get; private set; }
        public Guid? SelectedChoiceId { get; private set; }
        public List<Guid>? SelectedChoicesIds { get; private set; }

        private SurveyAnswer() { }

        //text question | yes/no question | slider question | star question
        public static SurveyAnswer CreateValue(Guid questionId, string value)
        {
            return new SurveyAnswer
            {
                QuestionId = questionId,
                Value = value
            };
        }

        public static SurveyAnswer CreateSingleChoice(Guid questionId, Guid choiceId)
        {
            return new SurveyAnswer
            {
                QuestionId = questionId,
                SelectedChoiceId = choiceId
            };
        }

        public static SurveyAnswer CreateMultipleChoices(Guid questionId, List<Guid> choices)
        {
            return new SurveyAnswer
            {
                QuestionId = questionId,
                SelectedChoicesIds = choices
            };
        }
    }
}
