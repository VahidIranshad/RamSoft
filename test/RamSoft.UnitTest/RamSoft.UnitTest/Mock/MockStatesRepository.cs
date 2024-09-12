using Moq;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;

namespace RamSoft.UnitTest.Mock
{
    public class MockStatesRepository
    {
        public static Mock<IStatesRepository> GetRepository()
        {
            var list = new List<States>()
            {
                new States{Id = 1, Name = "ToDo"}
            };

            var mockRepo = new Mock<IStatesRepository>();

            mockRepo.Setup(r => r.GetAll(default)).ReturnsAsync(list);

            mockRepo.Setup(r => r.Get(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken cancellation) =>
            {
                return list.Where(p => p.Id == id).First();
            });

            mockRepo.Setup(r => r.Add(It.IsAny<States>(), CancellationToken.None)).ReturnsAsync((States data, CancellationToken cancellation) =>
            {
                list.Add(data);
                return data;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<States>(), CancellationToken.None)).Callback((States data, CancellationToken cancellation) =>
            {
                var old = list.First(p => p.Id == data.Id);
                list.Remove(old);
                list.Add(data);
            });
            mockRepo.Setup(r => r.Delete(It.IsAny<int>(), CancellationToken.None)).Callback((int id, CancellationToken cancellation) =>
            {
                var item = list.First(p => p.Id == id);
                list.Remove(item);
            });
            mockRepo.Setup(r => r.Exists(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken cancellation) =>
            {
                return list.Any(p => p.Id == id);
            });

            return mockRepo;
        }
    }
}
