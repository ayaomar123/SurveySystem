namespace SurveySystem.Domain.Entites.Questions
{
    public sealed class QuestionChoice
    {
        public Guid Id { get; private set; }
        public Guid QuestionId { get; private set; }
        public string Text { get; private set; } = default!;
        public int Order { get; private set; }

        private QuestionChoice() { }

        public QuestionChoice(string text, int order)
        {
            Text = text;
            Order = order;
        }
    }
}
