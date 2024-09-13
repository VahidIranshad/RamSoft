using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Command.Create
{
    public record CreateTasksCommand : ICommand<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int TaskBoardId { get; set; }
        public int StatesId { get; set; }
    }
}
