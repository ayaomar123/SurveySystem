namespace SurveySystem.Domain.Entites.Questions
{
    public sealed class StarConfig
    {
        public Guid Id { get; private set; }
        public Guid QuestionId { get; private set; }
        public int MaxStar { get; private set; }

        private StarConfig() { }

        public StarConfig(int maxStar)
        {
            MaxStar = maxStar;
        }
    }
}
