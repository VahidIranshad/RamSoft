using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Command.UpdateState
{
    public record UpdateStatesTasksCommand : ICommand
    {
        public int Id { get; set; }
        public int StatesId { get; set; }
    }
}
