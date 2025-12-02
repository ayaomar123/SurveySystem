using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Questionns.Dtos;

namespace SurveySystem.Application.Questionns.Queries.GetQuestions
{
    public class GetQuestionsQueryHandler(IAppDbContext context) : IRequestHandler<GetQuestionsQuery, List<QuestionResponseDto>>
    {
        public async Task<List<QuestionResponseDto>> Handle(GetQuestionsQuery request, CancellationToken ct)
        {
            var query = context.Questions
                .Include(c => c.Choices)
                .Include(c => c.SliderConfig)
                .Include(c => c.StarConfig)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(q => q.Title.Contains(request.Title));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(q => q.Status == request.Status.Value);
            }

            var questions = await query
                .Select(question => new QuestionResponseDto(
                    question.Id,
                    question.Title,
                    question.Description,
                    (int)question.QuestionType,
                    question.IsRequired,
                    question.Status,
                    question.Choices,
                    question.SliderConfig,
                    question.StarConfig
                ))
                .ToListAsync(ct);

            return questions;
        }

    }

}
