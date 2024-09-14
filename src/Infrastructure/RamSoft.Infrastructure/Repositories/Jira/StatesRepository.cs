using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.DbContexts;
using RamSoft.Infrastructure.Repositories.Base;

namespace RamSoft.Infrastructure.Repositories.Jira
{
    public class StatesRepository : GenericRepository<States>, IStatesRepository
    {
        public StatesRepository(JiraDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
