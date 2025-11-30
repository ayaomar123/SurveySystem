using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Surveys.Responses.Dtos;
using SurveySystem.Domain.Entites.Questions.Enums;

namespace SurveySystem.Application.Surveys.Responses.Queries.GetSurveyResponse
{
    public class GetSurveyAnalyticsQueryHandler(IAppDbContext context) : IRequestHandler<GetSurveyAnalyticsQuery, SurveyAnalyticsDto>
    {
        public async Task<SurveyAnalyticsDto> Handle(GetSurveyAnalyticsQuery request, CancellationToken ct)
        {
            var survey = await context.Surveys
                .Include(s => s.SurveyQuestions)
                    .ThenInclude(sq => sq.Question)
                        .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(s => s.Id == request.SurveyId, ct);

            if (survey == null)
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

                var qDto = new QuestionAnalyticsDto
                {
                    QuestionId = sq.QuestionId,
                    Title = sq.Question.Title,
                    QuestionType = sq.Question.QuestionType
                };

                var qAnswers = responses
                    .SelectMany(r => r.Answers)
                    .Where(a => a.QuestionId == sq.QuestionId)
                    .ToList();

                qDto.TotalResponses = qAnswers.Count;

                switch (sq.Question.QuestionType)
                {
                    case QuestionType.TextInput:
                        qDto.TextValues = qAnswers
                            .Select(a => a.Value)
                            .Select(v => v!)
                            .ToList();
                        break;

                    case QuestionType.YesOrNo:
                        qDto.YesCount = qAnswers.Count(a => a.Value?.ToLower() == "yes");
                        qDto.NoCount = qAnswers.Count(a => a.Value?.ToLower() == "no");
                        break;

                    case QuestionType.Radio:
                        qDto.SingleChoiceValues = qAnswers
                            .GroupBy(a => a.SelectedChoiceId)
                            .ToDictionary(
                                g => sq.Question.Choices.First(c => c.Id == g.Key).Text,
                                g => g.Count()
                            );
                        break;

                    case QuestionType.Checkbox:
                        qDto.MultipleChoiceValues = qAnswers
                            .SelectMany(a => a.SelectedChoicesIds ?? new List<Guid>())
                            .GroupBy(id => id)
                            .ToDictionary(
                                g => sq.Question.Choices.First(c => c.Id == g.Key).Text,
                                g => g.Count()
                            );
                        break;

                    case QuestionType.Rating:
                        var ratings = qAnswers
                            .Select(a => int.Parse(a.Value!))
                            .ToList();

                        qDto.AverageRating = ratings.Any() ? ratings.Average() : 0;

                        qDto.RatingValues = ratings
                            .GroupBy(r => r)
                            .ToDictionary(g => g.Key, g => g.Count());
                        break;

                    case QuestionType.Slider:
                        var values = qAnswers
                            .Select(a => int.Parse(a.Value!))
                            .ToList();

                        qDto.AverageSlider = values.Any() ? values.Average() : 0;

                        qDto.SliderValues = values
                            .GroupBy(r => r)
                            .ToDictionary(g => g.Key, g => g.Count());
                        break;
                }

                dto.Questions.Add(qDto);
            }

            return dto;
        }
    }
}
