using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Delete
{
    public record DeleteTaskBoardStatesCommand : ICommand
    {
        public int Id { get; set; }
    }
}
