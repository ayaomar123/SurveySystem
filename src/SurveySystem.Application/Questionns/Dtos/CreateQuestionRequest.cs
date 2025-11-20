using SurveySystem.Application.Questions.Dtos;

namespace SurveySystem.Application.Questionns.Dtos
{
    public sealed class CreateQuestionRequest
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public QuestionTypeDto QuestionType { get; set; }
        public bool IsRequired { get; set; }
        public List<QuestionChoiceDto>? Choices { get; set; }
        public SliderConfigDto? Config { get; set; }
        public StarConfigDto? StarConfig { get; set; }
    }
}
