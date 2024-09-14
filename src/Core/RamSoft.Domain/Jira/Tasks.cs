using RamSoft.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace RamSoft.Domain.Jira
{
    public class Tasks : BaseEntity
    {
        [MaxLength(400)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int TaskBoardId { get; set; }
        
        public TaskBoard TaskBoard { get; set; }
        public int StatesId { get; set; }
        public States States { get; set; }
    }
}
