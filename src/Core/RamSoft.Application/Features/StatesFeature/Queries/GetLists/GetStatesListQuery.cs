using RamSoft.Application.Dtos.Jira.StatesDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.StatesFeature.Queries.GetLists
{
    public record GetStatesListQuery : IQuery<IList<StatesDto>>
    {
    }
}
