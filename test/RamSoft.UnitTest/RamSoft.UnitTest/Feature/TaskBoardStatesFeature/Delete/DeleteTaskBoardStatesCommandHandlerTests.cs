using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Delete;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TaskBoardStatesFeature.Delete
{
    public class DeleteTaskBoardStatesCommandHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly DeleteTaskBoardStatesCommandHandler _handler;
        public DeleteTaskBoardStatesCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new DeleteTaskBoardStatesCommandHandler(_mockUow.Object);

        }

        [Test]
        public async Task Happy_Scenario()
        {

            var _crudDto = new DeleteTaskBoardStatesCommand()
            {
                Id = 3,
            };
            var items = await _mockUow.Object.TaskBoardStatesRepository.GetAll(CancellationToken.None);
            var oldItemCounts = items.Count;
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            items = await _mockUow.Object.TaskBoardStatesRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == oldItemCounts - 1);
        }

        [Test]
        public async Task When_Id_Is_Not_Exist_Throw_Exception()
        {
            var command = new DeleteTaskBoardStatesCommand()
            {
                Id = 0,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_State_Is_Default_For_TaskBoard_Throw_Exception()
        {
            var command = new DeleteTaskBoardStatesCommand()
            {
                Id = 1,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
