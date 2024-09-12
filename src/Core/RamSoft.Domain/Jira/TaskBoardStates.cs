using RamSoft.Domain.Base;

namespace RamSoft.Domain.Jira
{
    public class TaskBoardStates : BaseEntity
    {
        public required TaskBoard TaskBoard { get; set; }
        public required States States { get; set; }

    }
}
