using RamSoft.Application.Dtos.Jira.StatesDtos;

namespace RamSoft.Application.Dtos.Jira.TaskBoardDtos
{
    public class TaskBoardDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefaultStatesId { get; set; }
        public string DefaultStatesName { get; set; }
    }
}
