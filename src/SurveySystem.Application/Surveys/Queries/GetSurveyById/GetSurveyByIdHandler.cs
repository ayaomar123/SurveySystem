using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Surveys.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveyById
{
    public sealed class GetSurveyByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetSurveyByIdQuery, SurveyDetailsDto>
    {

        public async Task<SurveyDetailsDto> Handle(GetSurveyByIdQuery request, CancellationToken ct)
        {
            var survey = await context.Surveys
                .Include(s => s.CreatedByUser)
                .Include(s => s.LastModifiedByUser)
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question)
                .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (survey is null)
                throw new Exception("Survey not found");

            var questions = survey.SurveyQuestions
                .OrderBy(q => q.Order)
                .Select(q => new SurveyQuestionDto(
                    //q.QuestionId,
                    q.Question!.Title,
                    (int)q.Question.QuestionType,
                    q.Order
                ))
                .ToList();

            return new SurveyDetailsDto(
                survey.Id,
                survey.Title,
                survey.Description,
                survey.Status,
                survey.StartDate,
                survey.EndDate,
                survey.CreatedAt,
                survey.LastModifiedDate,
                survey.CreatedBy,
                survey.CreatedByUser?.Name ?? string.Empty,
                survey.LastModifiedBy,
                survey.LastModifiedByUser?.Name,
                questions
            );
        }
    }
}
