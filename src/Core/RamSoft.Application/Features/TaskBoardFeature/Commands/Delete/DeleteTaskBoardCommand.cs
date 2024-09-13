
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Delete
{
    public record DeleteTaskBoardCommand : ICommand
    {
        public int Id { get; set; }
    }
}
