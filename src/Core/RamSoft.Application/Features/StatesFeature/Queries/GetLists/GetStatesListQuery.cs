using RamSoft.Application.Dtos.Jira.States;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.StatesFeature.Queries.GetLists
{
    public record GetStatesListQuery : IQuery<IList<StatesDto>>
    {
    }
}
