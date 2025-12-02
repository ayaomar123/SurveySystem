using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Questionns.Dtos;
using SurveySystem.Application.Surveys.Queries.GetSurveyById.Dtos;
using SurveySystem.Domain.Entites.Surveys.Enums;

namespace SurveySystem.Application.Surveys.Queries.GetSurveyById
{
    public sealed class GetSurveyByIdQueryHandler(IAppDbContext context, ICurrentUser user)
        : IRequestHandler<GetSurveyByIdQuery, SurveyDetailsDto>
    {

        public async Task<SurveyDetailsDto> Handle(GetSurveyByIdQuery request, CancellationToken ct)
        {
            var userId = user.UserId ?? null;

            var survey = await context.Surveys
                .Include(s => s.CreatedByUser)
                .Include(s => s.LastModifiedByUser)
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question)
                        .ThenInclude(q => q.Choices)
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question)
                        .ThenInclude(q => q.SliderConfig)
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question)
                        .ThenInclude(q => q.StarConfig)
                .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (survey is null || (survey.Status != SurveyStatus.Active && userId is null))
                throw new KeyNotFoundException("Survey not found");

            var questions = survey.SurveyQuestions
                .OrderBy(q => q.Order)
                .Select(q => new SurveyQuestionDetailsDto(
                    q.QuestionId,
                    q.Question!.Title,
                    q.Question.Description,
                    (int)q.Question.QuestionType,
                    q.Question.IsRequired,
                    q.Question.Choices?.OrderBy(c => c.Order).Select(c => new QuestionChoiceDto(
                        c.Id,
                        c.Text,
                        c.Order
                    )).ToList(),
                    q.Question.SliderConfig is null
                        ? null
                        : new SliderConfigDto(
                            q.Question.SliderConfig.Min,
                            q.Question.SliderConfig.Max,
                            q.Question.SliderConfig.Step,
                            q.Question.SliderConfig.UnitLabel
                        ),
                    q.Question.StarConfig is null
                        ? null
                        : new StarConfigDto(
                            q.Question.StarConfig.MaxStar
                        )
                ))
                .ToList();

            return new SurveyDetailsDto(
                survey.Id,
                survey.Title,
                survey.Description ?? "",
                survey.Status,
                survey.StartDate,
                survey.EndDate,
                survey.CreatedAt,
                survey.LastModifiedDate,
                survey.CreatedBy,
                survey.CreatedByUser?.Name ?? "",
                survey.LastModifiedBy,
                survey.LastModifiedByUser?.Name,
                questions
            );
        }
    }
}
