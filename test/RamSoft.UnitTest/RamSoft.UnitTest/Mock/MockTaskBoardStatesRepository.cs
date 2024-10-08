﻿using Moq;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Domain.Jira;
using System.Linq.Expressions;

namespace RamSoft.UnitTest.Mock
{
    public class MockTaskBoardStatesRepository
    {
        public static Mock<ITaskBoardStatesRepository> GetRepository()
        {
            var list = new List<TaskBoardStates>()
            {
                new TaskBoardStates{Id = 1, TaskBoardId = 1, StatesId = 1 },
                new TaskBoardStates{Id = 2, TaskBoardId = 1, StatesId = 2 },
                new TaskBoardStates{Id = 3, TaskBoardId = 1, StatesId = 3 }
            };

            var mockRepo = new Mock<ITaskBoardStatesRepository>();

            mockRepo.Setup(r => r.GetAll(CancellationToken.None, true)).ReturnsAsync((CancellationToken cancellation, bool disablaTracking) =>
                list.Where(p => p.IsDeleted == false).ToList()
                );


            mockRepo.Setup(r => r.Get(It.IsAny<int>(), CancellationToken.None, true)).ReturnsAsync((int id, CancellationToken cancellation, bool disablaTracking) =>
            {
                return list.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Expression<Func<TaskBoardStates, bool>>>(), CancellationToken.None, true)).ReturnsAsync((Expression<Func<TaskBoardStates, bool>> expression, CancellationToken cancellation, bool disablaTracking) =>
            {
                return list.AsQueryable().Where(expression).Where(p => p.IsDeleted == false).ToList();
            });

            mockRepo.Setup(r => r.GetListByTaskBoardId(It.IsAny<int>(), CancellationToken.None, true)).ReturnsAsync((int taskBoardId, CancellationToken cancellation, bool disablaTracking) =>
            {
                return list.Where(p => p.TaskBoardId == taskBoardId && p.IsDeleted == false).ToList();
            });


            mockRepo.Setup(r => r.Add(It.IsAny<TaskBoardStates>(), CancellationToken.None)).ReturnsAsync((TaskBoardStates data, CancellationToken cancellation) =>
            {
                list.Add(data);
                return data;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<TaskBoardStates>(), CancellationToken.None)).Callback((TaskBoardStates data, CancellationToken cancellation) =>
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
