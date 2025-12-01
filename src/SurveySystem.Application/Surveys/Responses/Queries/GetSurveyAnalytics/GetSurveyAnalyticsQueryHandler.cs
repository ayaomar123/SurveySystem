using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Surveys.Responses.Dtos;
using SurveySystem.Domain.Entites.Questions.Enums;

namespace SurveySystem.Application.Surveys.Responses.Queries.GetSurveyResponse
{
    public class GetSurveyAnalyticsQueryHandler(IAppDbContext context)
        : IRequestHandler<GetSurveyAnalyticsQuery, SurveyAnalyticsDto>
    {
        public async Task<SurveyAnalyticsDto> Handle(GetSurveyAnalyticsQuery request, CancellationToken ct)
        {
            var survey = await context.Surveys
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question)
                        .ThenInclude(q => q.Choices)
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question.SliderConfig)
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question.StarConfig)
                .FirstOrDefaultAsync(s => s.Id == request.SurveyId, ct);

            if (survey is null)
                throw new Exception("Survey not found.");

            var responses = await context.SurveyResponses
                .Include(r => r.Answers)
                .Where(r => r.SurveyId == request.SurveyId)
                .ToListAsync(ct);

            var dto = new SurveyAnalyticsDto
            {
                Title = survey.Title,
                TotalResponses = responses.Count
            };

            foreach (var sq in survey.SurveyQuestions)
            {
                var q = sq.Question;

                var qDto = new QuestionAnalyticsDto
                {
                    QuestionId = q.Id,
                    Title = q.Title,
                    QuestionType = q.QuestionType
                };

                var qAnswers = responses
                    .SelectMany(r => r.Answers)
                    .Where(a => a.QuestionId == q.Id)
                    .ToList();

                qDto.TotalResponses = qAnswers.Count;

                switch (q.QuestionType)
                {
                    case QuestionType.TextInput:
                        qDto.TextValues = qAnswers
                            .Select(a => a.Value)
                            .Where(v => !string.IsNullOrWhiteSpace(v))
                            .ToList();
                        break;

                    case QuestionType.YesOrNo:
                        qDto.YesCount = qAnswers.Count(a => a.Value?.ToLower() == "yes");
                        qDto.NoCount = qAnswers.Count(a => a.Value?.ToLower() == "no");
                        break;

                    case QuestionType.Radio:
                        qDto.SingleChoiceValues = q.Choices
                            .ToDictionary(c => c.Text, c => 0);

                        foreach (var answer in qAnswers.Where(a => a.SelectedChoiceId != null))
                        {
                            var choice = q.Choices.FirstOrDefault(c => c.Id == answer.SelectedChoiceId);
                            if (choice != null)
                                qDto.SingleChoiceValues[choice.Text]++;
                        }
                        break;

                    case QuestionType.Checkbox:
                        qDto.MultipleChoiceValues = q.Choices
                            .ToDictionary(c => c.Text, c => 0);

                        foreach (var answer in qAnswers)
                        {
                            foreach (var selectedId in answer.SelectedChoicesIds ?? new List<Guid>())
                            {
                                var choice = q.Choices.FirstOrDefault(c => c.Id == selectedId);
                                if (choice != null)
                                    qDto.MultipleChoiceValues[choice.Text]++;
                            }
                        }
                        break;

                    case QuestionType.Rating:
                        int maxStar = q.StarConfig?.MaxStar ?? 5;

                        var numericRatings = qAnswers
                            .Select(a =>
                            {
                                int.TryParse(a.Value, out var r);
                                return r;
                            })
                            .Where(r => r > 0)
                            .ToList();

                        qDto.AverageRating = numericRatings.Any()
                            ? numericRatings.Average()
                            : 0;

                        qDto.RatingValues = Enumerable
                            .Range(1, maxStar)
                            .ToDictionary(
                                star => star,
                                star => numericRatings.Count(r => r == star)
                            );

                        break;

                    case QuestionType.Slider:
                        var sliderValues = qAnswers
                            .Select(a =>
                            {
                                int.TryParse(a.Value, out var v);
                                return v;
                            })
                            .Where(v => v >= 0)
                            .ToList();

                        qDto.AverageSlider = sliderValues.Any()
                            ? sliderValues.Average()
                            : 0;

                        qDto.SliderValues = sliderValues
                            .GroupBy(v => v)
                            .ToDictionary(g => g.Key, g => g.Count());

                        break;
                }

                dto.Questions.Add(qDto);
            }

            return dto;
        }
    }
}
