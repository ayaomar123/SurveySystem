using MediatR;
using SurveySystem.Application.Questionns.Dtos;

namespace SurveySystem.Application.Questionns.Queries.GetQuestions
{
    public sealed record GetQuestionsQuery() : IRequest<List<QuestionResponseDto>>;
}
