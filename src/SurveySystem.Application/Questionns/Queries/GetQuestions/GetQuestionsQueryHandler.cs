using MediatR;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Questionns.Dtos;

namespace SurveySystem.Application.Questionns.Queries.GetQuestions
{
    public class GetQuestionsQueryHandler(IAppDbContext context) : IRequestHandler<GetQuestionsQuery, List<QuestionResponseDto>>
    {
        public Task<List<QuestionResponseDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
