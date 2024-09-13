using RamSoft.Application.Dtos.Jira.TaskBoardStatesDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Queries.GetListByTaskBoardID
{
    public record GetTaskBoardStatesListByTaskBoardIdQuery : IQuery<IList<TaskBoardStateDto>>
    {
        public int TaskBoardId { get; set; }
    }
}
