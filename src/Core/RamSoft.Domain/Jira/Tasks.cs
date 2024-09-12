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
        public required TaskBoard TaskBoard { get; set; }
        public required States States { get; set; }
    }
}
