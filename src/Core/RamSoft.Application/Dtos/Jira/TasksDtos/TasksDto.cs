namespace RamSoft.Application.Dtos.Jira.TasksDtos
{
    public class TasksDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int TaskBoardId { get; set; }
        public string TaskBoardName { get; set; }
        public int StatesId { get; set; }
        public string StatesName { get; set; }
    }
}
