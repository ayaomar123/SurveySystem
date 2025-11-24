using MediatR;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Commands.CreateSurvey
{
    public sealed record SurveyQuestionItem(Guid QuestionId, int Order);

    public sealed record CreateSurveyCommand(
        string Title,
        string? Description,
        SurveyStatus Status,
        DateTime? StartDate,
        DateTime? EndDate,
        List<SurveyQuestionItem> Questions
    ) : IRequest<Guid>;
}
