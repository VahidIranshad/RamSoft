using RamSoft.Application.Dtos.Jira.StatesDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Update
{
    public record UpdateTaskBoardCommand : ICommand
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DefaultStatesId { get; set; }
    }
}
