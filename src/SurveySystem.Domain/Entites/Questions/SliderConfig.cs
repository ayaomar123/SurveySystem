namespace SurveySystem.Domain.Entites.Questions
{
    public sealed class SliderConfig
    {
        public Guid Id { get; private set; }
        public Guid QuestionId { get; private set; }
        public int Max { get; private set; }
        public int Min { get; private set; }
        public int Step { get; private set; }
        public string UnitLabel { get; private set; } = string.Empty;

        private SliderConfig() { }

        public SliderConfig(int min, int max, int step, string? label)
        {
            Min = min;
            Max = max;
            Step = step;
            UnitLabel = label ?? "";
        }

    }
}
