using SurveySystem.Domain.Entites.Questions.Enums;

namespace SurveySystem.Domain.Entites.Questions
{
    public class Question
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public QuestionType QuestionType { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public bool IsRequired { get; private set; }
        public bool Status { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public List<QuestionChoice> Choices { get; private set; } = new();
        public SliderConfig? SliderConfig { get; private set; }
        public StarConfig? StarConfig { get; private set; }

        private Question() { }

        private Question(
            string title,
            QuestionType type,
            string? description,
            bool required)
        {
            Title = title;
            QuestionType = type;
            Description = description;
            IsRequired = required;
        }


        public static Question CreateTextQuestion(
            string title,
            string? description,
            bool required)
        {
            return new Question(title, QuestionType.TextInput, description, required);
        }

        public static Question CreateYesNoQuestion(
            string title,
            string? description,
            bool required)
        {
            return new Question(title, QuestionType.YesOrNo, description, required);
        }

        public static Question CreateChoiceQuestion(
            string title,
            QuestionType type,
            string? description,
            bool required,
            List<QuestionChoice> choices)
        {
            var q = new Question(title, type, description, required);
            q.Choices = choices;

            return q;
        }

        public static Question CreateSliderQuestion(
            string title,
            string? description,
            bool required,
            SliderConfig config)
        {
            var q = new Question(title, QuestionType.Slider, description, required);
            q.SliderConfig = config;

            return q;
        }

        public static Question CreateRatingQuestion(
            string title,
            string? description,
            bool required,
            StarConfig stars)
        {
            var q = new Question(title, QuestionType.Rating, description, required);
            q.StarConfig = stars;

            return q;
        }
    }
}
