using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using RamSoft.Application.Contracts.Base;
using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.Repositories.Jira;
using System.Linq.Expressions;

namespace RamSoft.InfrastructureTests.Repositories
{
    [TestFixture]
    public class TaskBoardStatesRepositoryTests
    {
        private SharedDatabaseFixture Fixture { get; set; }
        private IMapper _mapper;
        private ICurrentUserService _currentUser = null;

        [SetUp]
        public void Setup()
        {
            Fixture = new SharedDatabaseFixture();
            _mapper = SharedDatabaseFixture.mapper;
        }

        [Test]
        public async Task GetAll_Between_Repository_And_Context()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new TaskBoardStatesRepository(context);

                var list = await repository.GetAll(CancellationToken.None);
                var dbList = await context.TaskBoardStatesDbSet.ToListAsync(CancellationToken.None);

                CollectionAssert.AreEqual(list, dbList);
            }
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(5)]
        public async Task Get_By_Id_Between_Repository_And_Context(int id)
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new TaskBoardStatesRepository(context);

                var item = await repository.Get(id, CancellationToken.None);
                var dbItem = await context.TaskBoardStatesDbSet.AsNoTracking().Where(p => p.Id == id).FirstOrDefaultAsync(CancellationToken.None);

                Assert.That(item, Is.EqualTo(dbItem));
            }
        }

        [Test]
        public async Task Get_By_Expression_Between_Repository_And_Context()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new TaskBoardStatesRepository(context);

                Expression<Func<TaskBoardStates, bool>> expression = (p => p.Id > 0);

                var list = await repository.Get(expression, CancellationToken.None);
                var dbList = await context.TaskBoardStatesDbSet.Where(expression).ToListAsync(CancellationToken.None);

                CollectionAssert.AreEqual(list, dbList);
            }
        }

        [Test]
        public async Task Get_By_Ids_Between_Repository_And_Context()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new TaskBoardStatesRepository(context);

                var ids = new List<int>() { 1, 2, 3, 5 };

                var list = await repository.Get(ids, CancellationToken.None);
                var dbList = await context.TaskBoardStatesDbSet.Where(p => ids.Contains(p.Id)).ToListAsync(CancellationToken.None);

                CollectionAssert.AreEqual(list, dbList);
            }
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(5)]
        public async Task Exists_Between_Repository_And_Context(int id)
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new TaskBoardStatesRepository(context);

                var item = await repository.Exists(id, CancellationToken.None);
                var dbItem = await context.TaskBoardStatesDbSet.AsNoTracking().Where(p => p.Id == id).AnyAsync(CancellationToken.None);

                Assert.That(item, Is.EqualTo(dbItem));
            }
        }

        
        public async Task Add_Item()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new TaskBoardStatesRepository(context);

                var newItem = new TaskBoardStates() { TaskBoardId = 1, StatesId = 2};

                var item = await repository.Add(newItem, CancellationToken.None);
                var dbItem = await context.TaskBoardStatesDbSet.Where(p => p.Id == item.Id).FirstOrDefaultAsync(CancellationToken.None);

                Assert.That(item, Is.EqualTo(dbItem));
            }
        }

        public async Task Delete_Item()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new TaskBoardStatesRepository(context);

                await repository.Delete(1, CancellationToken.None);
                var dbItem = await context.TaskBoardStatesDbSet.Where(p => p.Id == 1).FirstOrDefaultAsync(CancellationToken.None);

                Assert.That(dbItem, Is.EqualTo(null));
            }
        }

        [TearDown]
        public void TearDown()
        {
            Fixture.Dispose();
        }
    }
}
