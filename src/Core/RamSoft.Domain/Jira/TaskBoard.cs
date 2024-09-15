using RamSoft.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace RamSoft.Domain.Jira
{
    public class TaskBoard : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
        public List<TaskBoardStates>? TaskBoardStateList { get; set; } = new List<TaskBoardStates>();
        public int DefaultStatesId { get; set; }
        public States DefaultStates { get; set; }


        public ICollection<Tasks> Tasks { get; set; }
    }
}
