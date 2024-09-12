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

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(list);

            mockRepo.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((int id) =>
            {
                return list.Where(p => p.Id == id).First();
            });

            mockRepo.Setup(r => r.Add(It.IsAny<States>())).ReturnsAsync((States data) =>
            {
                list.Add(data);
                return data;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<States>())).Callback((States data) =>
            {
                var old = list.First(p => p.Id == data.Id);
                list.Remove(old);
                list.Add(data);
            });
            mockRepo.Setup(r => r.Delete(It.IsAny<int>())).Callback((int id) =>
            {
                var item = list.First(p => p.Id == id);
                list.Remove(item);
            });

            return mockRepo;
        }
    }
}
