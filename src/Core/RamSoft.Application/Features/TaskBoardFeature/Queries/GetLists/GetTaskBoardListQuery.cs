using RamSoft.Application.Dtos.Jira.TaskBoardDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Queries.GetLists
{
    public record GetTaskBoardListQuery : IQuery<IList<TaskBoardDto>>
    {
    }
}
