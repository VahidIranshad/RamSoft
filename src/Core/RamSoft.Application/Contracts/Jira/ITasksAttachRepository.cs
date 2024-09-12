using RamSoft.Application.Contracts.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Contracts.Jira
{
    public interface ITasksAttachRepository : IGenericRepository<TasksAttach, int>
    {
    }
}
