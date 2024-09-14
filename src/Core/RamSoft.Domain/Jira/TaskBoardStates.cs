using RamSoft.Domain.Base;

namespace RamSoft.Domain.Jira
{
    public class TaskBoardStates : BaseEntity
    {
        public int TaskBoardId { get; set; }
        public TaskBoard TaskBoard { get; set; }
        public int StatesId { get; set; }
        public States States { get; set; }
        public int OrderShow { get; set; } = 1;

    }
}
