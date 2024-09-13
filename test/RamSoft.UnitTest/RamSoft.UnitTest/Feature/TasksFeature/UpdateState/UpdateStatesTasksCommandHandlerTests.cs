using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TasksFeature.Command.UpdateState;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TasksFeature.UpdateState
{
    public class UpdateStatesTasksCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly UpdateStatesTasksCommandHandler _handler;
        private readonly UpdateStatesTasksValidation _validator;
        public UpdateStatesTasksCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new UpdateStatesTasksValidation(_mockUow.Object, CancellationToken.None);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateStatesTasksCommandHandler(_mockUow.Object, _mapper);

        }



        [Test]
        public async Task Happy_Scenario()
        {
            var _crudDto = new UpdateStatesTasksCommand()
            {
                Id = 1,
                StatesId = 3,
            };
            var result = await _handler.Handle(_crudDto, CancellationToken.None);

            var item = await _mockUow.Object.TasksRepository.Get(_crudDto.Id, CancellationToken.None);
            Assert.That(item.Id == _crudDto.Id);
            Assert.That(item.StatesId == _crudDto.StatesId);
        }

        [Test]
        public async Task When_StatesId_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateStatesTasksCommand()
            {
                Id = 1,
                StatesId = 0,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_Id_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateStatesTasksCommand()
            {
                Id = 0,
                StatesId = 2,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_TaskBoardId_Is_Not_Related_To_StatesId_Throw_CustomValidationException()
        {
            var command = new UpdateStatesTasksCommand()
            {
                Id = 1,
                StatesId = 4,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
