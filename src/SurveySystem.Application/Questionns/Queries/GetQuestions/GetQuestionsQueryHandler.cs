using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Questionns.Dtos;
using System.Linq;

namespace SurveySystem.Application.Questionns.Queries.GetQuestions
{
    public class GetQuestionsQueryHandler(IAppDbContext context) : IRequestHandler<GetQuestionsQuery, List<QuestionResponseDto>>
    {
        public async Task<List<QuestionResponseDto>> Handle(GetQuestionsQuery request, CancellationToken ct)
        {
            var questions = await context.Questions
                .Include(c => c.Choices)
                .Include(c => c.SliderConfig)
                .Include(c => c.StarConfig)
                .Select(question => new QuestionResponseDto(question.Id,
                                                            question.Title,
                                                            question.Description,
                                                            (int)question.QuestionType,
                                                            question.IsRequired,
                                                            question.Status,
                                                            question.CreatedAt,
                                                            question.Choices, // بدهم ترتيب
                                                            question.SliderConfig,
                                                            question.StarConfig))
                .ToListAsync(ct);

            return questions;
        }
    }

}
