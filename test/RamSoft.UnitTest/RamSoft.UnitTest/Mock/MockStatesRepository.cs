using Moq;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using System.Linq.Expressions;

namespace RamSoft.UnitTest.Mock
{
    public class MockStatesRepository
    {
        public static Mock<IStatesRepository> GetRepository()
        {
            var list = new List<States>()
            {
                new States{Id = 1, Name = "ToDo"},
                new States{Id = 2, Name = "InProgress"},
                new States{Id = 3, Name = "Review"},
                new States{Id = 4, Name = "Done"},
            };

            var mockRepo = new Mock<IStatesRepository>();

            mockRepo.Setup(r => r.GetAll(CancellationToken.None)).ReturnsAsync((CancellationToken cancellation) =>
                list.Where(p => p.IsDeleted == false).ToList()
                );

            mockRepo.Setup(r => r.Get(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken cancellation) =>
            {
                return list.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<States, bool>>>(), CancellationToken.None)).ReturnsAsync((Expression<Func<States, bool>> expression, CancellationToken cancellation) =>
            {
                return list.AsQueryable().Where(expression).Where(p => p.IsDeleted == false).ToList();
            });

            mockRepo.Setup(r => r.Add(It.IsAny<States>(), CancellationToken.None)).ReturnsAsync((States data, CancellationToken cancellation) =>
            {
                list.Add(data);
                return data;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<States>(), CancellationToken.None)).Callback((States data, CancellationToken cancellation) =>
            {
                var old = list.First(p => p.Id == data.Id && p.IsDeleted == false);
                list.Remove(old);
                list.Add(data);
            });
            mockRepo.Setup(r => r.Delete(It.IsAny<int>(), CancellationToken.None)).Callback((int id, CancellationToken cancellation) =>
            {
                var item = list.First(p => p.Id == id && p.IsDeleted == false);
                list.Remove(item);
                item.IsDeleted = true;
                list.Add(item);
            });
            mockRepo.Setup(r => r.Exists(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken cancellation) =>
            {
                return list.Any(p => p.Id == id && p.IsDeleted == false);
            });

            return mockRepo;
        }
    }
}
