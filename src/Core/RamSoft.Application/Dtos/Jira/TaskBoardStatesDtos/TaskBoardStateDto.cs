namespace RamSoft.Application.Dtos.Jira.TaskBoardStatesDtos
{
    public class TaskBoardStateDto
    {
        public int TaskBoardId { get; set; }
        public int StatesId { get; set; }
        public string StatesName { get; set; }
        public int OrderShow { get; set; } = 1;
    }
}
