using RamSoft.Application.Contracts.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Contracts.Jira
{
    public interface ITasksRepository : IGenericRepository<Tasks>
    {
        Task<IReadOnlyList<Tasks>> GetListByTaskBoardId(int TaskBoardId, CancellationToken cancellationToken);
        Task<bool> Exists(int TaskBoardId, int statesId , CancellationToken cancellationToken);
    }
}
