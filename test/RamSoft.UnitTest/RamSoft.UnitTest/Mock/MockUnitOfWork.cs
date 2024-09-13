using Moq;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.UnitTest.Mock
{
    internal class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var _StatesRepository = MockStatesRepository.GetRepository();
            var _TaskBoardStatesRepository = MockTaskBoardStatesRepository.GetRepository();
            var _TaskBoardRepository = MockTaskBoardRepository.GetRepository();

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(p => p.StatesRepository).Returns(() => { return _StatesRepository.Object; });
            mockUow.Setup(p => p.TaskBoardStatesRepository).Returns(() => { return _TaskBoardStatesRepository.Object; });
            mockUow.Setup(p => p.TaskBoardRepository).Returns(() => { return _TaskBoardRepository.Object; });
            return mockUow;
        }
    }
}
