using RamSoft.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace RamSoft.Domain.Jira
{
    public class TaskBoard : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }
        public ICollection<TaskBoardStates>? TaskBoardStateList { get; set; }
        public required States DefaultState { get; set; }
    }
}
