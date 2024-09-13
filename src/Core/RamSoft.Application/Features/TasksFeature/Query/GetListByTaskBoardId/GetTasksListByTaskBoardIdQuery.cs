using RamSoft.Application.Dtos.Jira.TasksDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Query.GetListByTaskBoardId
{
    public record GetTasksListByTaskBoardIdQuery : IQuery<IList<TasksDto>>
    {
        public int TaskBoardId { get; set; }
    }
}
