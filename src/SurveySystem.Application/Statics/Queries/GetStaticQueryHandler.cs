
using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Application.Interfaces;
using SurveySystem.Application.Statics.Dtos;

namespace SurveySystem.Application.Statics.Queries
{
    public class GetStaticQueryHandler(IAppDbContext context) : IRequestHandler<GetStaticQuery, StaticDto>
    {
        public async Task<StaticDto> Handle(GetStaticQuery request, CancellationToken cancellationToken)
        {
            var responses = context.SurveyResponses.Count();
            var surveys = context.Surveys.Count();
            var activeSurveys = 
                context.Surveys
                .Where(a => a.Status == Domain.Entites.Surveys.Enums.SurveyStatus.Active).Count();
            var questions = context.Questions.Count();

            var surveysList = await context.Surveys
                .Where(s => s.SurveyResponses.Any())
                .Select(a => new ResponseDto(a.Id, a.Title,a.SurveyResponses.Count))
                .ToListAsync();

            return new StaticDto(responses, surveys, activeSurveys, questions, surveysList);
        }
    }
}
