using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Command.Delete
{
    public record DeleteTasksCommand : ICommand
    {
        public int Id { get; set; }
    }
}
