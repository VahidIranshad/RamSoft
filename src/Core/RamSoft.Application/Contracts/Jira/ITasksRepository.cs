using RamSoft.Application.Contracts.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Contracts.Jira
{
    public interface ITasksRepository : IGenericRepository<Tasks>
    {
        Task<IReadOnlyList<Tasks>> GetListByTaskBoardId(int taskBoardId, CancellationToken cancellationToken, bool disablaTracking = true);
        Task<bool> Exists(int taskBoardId, int statesId , CancellationToken cancellationToken);
    }
}
