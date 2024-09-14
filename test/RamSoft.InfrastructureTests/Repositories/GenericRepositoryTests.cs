using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Internal;
using RamSoft.Application.Contracts.Base;
using RamSoft.Domain.Jira;
using RamSoft.Infrastructure.Repositories.Base;
using RamSoft.Infrastructure.Repositories.Jira;
using System.Linq.Expressions;

namespace RamSoft.InfrastructureTests.Repositories
{
    [TestFixture]
    public class GenericRepositoryTests
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
                var repository = new StatesRepository(context);

                var list = await repository.GetAll(CancellationToken.None);
                var dbList = await context.StatesDbSet.ToListAsync(CancellationToken.None);

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
                var repository = new StatesRepository(context);

                var item = await repository.Get(id, CancellationToken.None);
                var dbItem = await context.StatesDbSet.Where(p => p.Id == id).FirstOrDefaultAsync(CancellationToken.None);

                Assert.That(item, Is.EqualTo(dbItem));
            }
        }

        [Test]
        public async Task Get_By_Expression_Between_Repository_And_Context()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new StatesRepository(context);

                Expression<Func<States, bool>> expression = (p => p.Id > 0);

                var list = await repository.Get(expression, CancellationToken.None);
                var dbList = await context.StatesDbSet.Where(expression).ToListAsync(CancellationToken.None);

                CollectionAssert.AreEqual(list, dbList);
            }
        }

        [Test]
        public async Task Get_By_Ids_Between_Repository_And_Context()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new StatesRepository(context);

                var ids = new List<int>() { 1, 2, 3, 5 };

                var list = await repository.Get(ids, CancellationToken.None);
                var dbList = await context.StatesDbSet.Where(p => ids.Contains(p.Id)).ToListAsync(CancellationToken.None);

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
                var repository = new StatesRepository(context);

                var item = await repository.Exists(id, CancellationToken.None);
                var dbItem = await context.StatesDbSet.Where(p => p.Id == id).AnyAsync(CancellationToken.None);

                Assert.That(item, Is.EqualTo(dbItem));
            }
        }

        
        public async Task Add_Item()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new StatesRepository(context);

                var newItem = new States() { Name = "test" };

                var item = await repository.Add(newItem, CancellationToken.None);
                var dbItem = await context.StatesDbSet.Where(p => p.Id == item.Id).FirstOrDefaultAsync(CancellationToken.None);

                Assert.That(item, Is.EqualTo(dbItem));
            }
        }

        public async Task Update_Item()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new StatesRepository(context);

                var updateItem = new States() { Id = 1, Name = "test" };

                await repository.Update(updateItem, CancellationToken.None);
                var dbItem = await context.StatesDbSet.Where(p => p.Id == updateItem.Id).FirstOrDefaultAsync(CancellationToken.None);

                Assert.That(updateItem, Is.EqualTo(dbItem));
            }
        }

        public async Task Delete_Item()
        {
            using (var context = Fixture.CreateSQLContext())
            {
                var repository = new StatesRepository(context);

                await repository.Delete(1, CancellationToken.None);
                var dbItem = await context.StatesDbSet.Where(p => p.Id == 1).FirstOrDefaultAsync(CancellationToken.None);

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
