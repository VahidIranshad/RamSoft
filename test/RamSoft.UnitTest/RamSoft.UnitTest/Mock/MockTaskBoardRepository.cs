using Moq;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using System.Linq.Expressions;

namespace RamSoft.UnitTest.Mock
{
    public class MockTaskBoardRepository
    {

        public static Mock<ITaskBoardRepository> GetRepository(ITaskBoardStatesRepository _TaskBoardStatesRepository)
        {
            var list = new List<TaskBoard>()
            {
                new TaskBoard{Id = 1, Name = "ToDo", DefaultStatesId = 1 }
            };

            var mockRepo = new Mock<ITaskBoardRepository>();

            mockRepo.Setup(r => r.GetAll(CancellationToken.None, true)).ReturnsAsync((CancellationToken cancellation, bool disablaTracking) =>
                list.Where(p => p.IsDeleted == false).ToList()
                );


            mockRepo.Setup(r => r.Get(It.IsAny<int>(), CancellationToken.None, true)).ReturnsAsync((int id, CancellationToken cancellation, bool disablaTracking) =>
            {
                return list.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<TaskBoard, bool>>>(), CancellationToken.None, true)).ReturnsAsync((Expression<Func<TaskBoard, bool>> expression, CancellationToken cancellation, bool disablaTracking) =>
            {
                return list.AsQueryable().Where(expression).Where(p => p.IsDeleted == false).ToList();
            });

            mockRepo.Setup(r => r.Add(It.IsAny<TaskBoard>(), CancellationToken.None)).ReturnsAsync((TaskBoard data, CancellationToken cancellation) =>
            {
                list.Add(data);
                _TaskBoardStatesRepository.Add( data.TaskBoardStateList.First(), cancellation);
                return data;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<TaskBoard>(), CancellationToken.None)).Callback((TaskBoard data, CancellationToken cancellation) =>
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
