using MediatR;
using SurveySystem.Application.Surveys.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveyById
{
    public sealed record GetSurveyByIdQuery(Guid Id) : IRequest<SurveyDetailsDto>;
}
