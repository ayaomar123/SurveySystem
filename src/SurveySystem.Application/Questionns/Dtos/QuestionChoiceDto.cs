namespace SurveySystem.Application.Questionns.Dtos
{
    public sealed record QuestionChoiceDto(
        Guid Id,
        string Text,
        int Order
);
}
