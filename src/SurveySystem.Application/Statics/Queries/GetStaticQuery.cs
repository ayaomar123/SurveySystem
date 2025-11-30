using MediatR;
using SurveySystem.Application.Statics.Dtos;

namespace SurveySystem.Application.Statics.Queries
{
    public sealed class GetStaticQuery : IRequest<StaticDto>
    {
    }
}
