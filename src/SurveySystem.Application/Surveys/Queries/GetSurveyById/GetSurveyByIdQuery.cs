using MediatR;
using SurveySystem.Application.Surveys.Dtos.Details;

namespace SurveySystem.Application.Surveys.Queries.GetSurveyById
{
    public sealed record GetSurveyByIdQuery(Guid Id)
        : IRequest<SurveyDetailsDto>;
}
