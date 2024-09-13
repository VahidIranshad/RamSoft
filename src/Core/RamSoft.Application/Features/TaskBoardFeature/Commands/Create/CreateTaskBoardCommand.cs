using RamSoft.Application.Dtos.Jira.StatesDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Create
{
    public record CreateTaskBoardCommand : ICommand<int>
    {
        public required string Name { get; set; }
        public int DefaultStatesId { get; set; }
    }
}
