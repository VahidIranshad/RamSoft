using Moq;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RamSoft.UnitTest.Mock
{
    internal class MockTasksRepository
    {

        public static Mock<ITasksRepository> GetRepository()
        {
            var list = new List<Tasks>()
            {
                new Tasks{Id = 1, Name = "new task", Description = "Description", TaskBoardId = 1, StatesId = 2, Deadline = DateTime.Now }
            };

            var mockRepo = new Mock<ITasksRepository>();

            mockRepo.Setup(r => r.GetAll(CancellationToken.None)).ReturnsAsync((CancellationToken cancellation) =>
                list.Where(p => p.IsDeleted == false).ToList()
                );


            mockRepo.Setup(r => r.Get(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int id, CancellationToken cancellation) =>
            {
                return list.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<Tasks, bool>>>(), CancellationToken.None)).ReturnsAsync((Expression<Func<Tasks, bool>> expression, CancellationToken cancellation) =>
            {
                return list.AsQueryable().Where(expression).Where(p => p.IsDeleted == false).ToList();
            });

            mockRepo.Setup(r => r.GetListByTaskBoardId(It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int taskBoardId, CancellationToken cancellation) =>
            {
                return list.Where(p => p.TaskBoardId == taskBoardId && p.IsDeleted == false).ToList();
            });

            mockRepo.Setup(r => r.Add(It.IsAny<Tasks>(), CancellationToken.None)).ReturnsAsync((Tasks data, CancellationToken cancellation) =>
            {
                list.Add(data);
                return data;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<Tasks>(), CancellationToken.None)).Callback((Tasks data, CancellationToken cancellation) =>
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
            mockRepo.Setup(r => r.Exists(It.IsAny<int>(), It.IsAny<int>(), CancellationToken.None)).ReturnsAsync((int taskBoardId, int statesId, CancellationToken cancellation) =>
            {
                return list.Any(p => p.TaskBoardId == taskBoardId && p.StatesId == statesId && p.IsDeleted == false);
            });

            return mockRepo;
        }
    }
}
