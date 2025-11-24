using MediatR;
using SurveySystem.Application.Surveys.Commands.CreateSurvey;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Commands.UpdateSurvey
{
    public sealed record UpdateSurveyCommand(
        Guid Id,
        string Title,
        string? Description,
        SurveyStatus Status,
        DateTime? StartDate,
        DateTime? EndDate,
        List<SurveyQuestionItem> Questions
    ) : IRequest<Unit>;
}
