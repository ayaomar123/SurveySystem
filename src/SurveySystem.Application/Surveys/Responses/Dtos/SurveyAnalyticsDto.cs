using SurveySystem.Domain.Entites.Questions.Enums;

namespace SurveySystem.Application.Surveys.Responses.Dtos
{
    public class SurveyAnalyticsDto
    {
        public string Title { get; set; } = default!;

        public int TotalResponses { get; set; }

        public List<QuestionAnalyticsDto> Questions { get; set; } = new();
    }

    public class QuestionAnalyticsDto
    {
        public Guid QuestionId { get; set; }
        public string Title { get; set; } = default!;
        public QuestionType QuestionType { get; set; }

        public int TotalResponses { get; set; }

        public List<string>? TextValues { get; set; }

        public double? AverageRating { get; set; }
        public Dictionary<int, int>? RatingValues { get; set; }

        public double? AverageSlider { get; set; }
        public Dictionary<int, int>? SliderValues { get; set; }

        public int? YesCount { get; set; }
        public int? NoCount { get; set; }

        public Dictionary<string, int>? SingleChoiceValues { get; set; }

        public Dictionary<string, int>? MultipleChoiceValues { get; set; }
    }
}
