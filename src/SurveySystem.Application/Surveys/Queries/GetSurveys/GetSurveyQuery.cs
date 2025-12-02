using MediatR;
using SurveySystem.Application.Surveys.Queries.GetSurveys.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveys
{
    public sealed record GetSurveyQuery(GetSurveyQueryDto? Request) 
        : IRequest<List<GetSurveysResponseDto>>;
}
