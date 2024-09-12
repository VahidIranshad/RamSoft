using Moq;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.UnitTest.Mock
{
    internal class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var _StatesRepository = MockStatesRepository.GetRepository();
            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(p => p.StatesRepository).Returns(() => { return _StatesRepository.Object; });
            return mockUow;
        }
    }
}
