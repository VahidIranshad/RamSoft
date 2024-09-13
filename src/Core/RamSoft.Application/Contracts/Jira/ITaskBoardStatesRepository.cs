using RamSoft.Application.Contracts.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Contracts.Jira
{
    public interface ITaskBoardStatesRepository : IGenericRepository<TaskBoardStates>
    {
        Task<IReadOnlyList<TaskBoardStates>> GetListByTaskBoardId(int taskBoardId, CancellationToken cancellationToken);
        Task<bool> Exists(int taskBoardStatesId, int statesId , CancellationToken cancellationToken);
    }
}
