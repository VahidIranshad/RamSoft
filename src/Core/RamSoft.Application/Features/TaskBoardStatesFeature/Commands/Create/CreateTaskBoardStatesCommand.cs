using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Create
{
    public record CreateTaskBoardStatesCommand : ICommand<int>
    {
        public int TaskBoardId { get; set; }
        public int StatesId { get; set; }
        public int OrderShow { get; set; }
    }
}
