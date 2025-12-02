using MediatR;
using SurveySystem.Application.Surveys.Queries.GetSurveyById.Dtos;

namespace SurveySystem.Application.Surveys.Queries.GetSurveyById
{
    public sealed record GetSurveyByIdQuery(Guid Id)
        : IRequest<SurveyDetailsDto>;
}
