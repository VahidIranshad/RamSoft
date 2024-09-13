using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Update;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TaskBoardFeature.Update
{
    public class UpdateTaskBoardCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly UpdateTaskBoardCommand _crudDto;
        private readonly UpdateTaskBoardCommandHandler _handler;
        private readonly UpdateTaskBoardValidation _validator;
        public UpdateTaskBoardCommandHandlerTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new UpdateTaskBoardValidation(_mockUow.Object, CancellationToken.None);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateTaskBoardCommandHandler(_mockUow.Object, _mapper);

            _crudDto = new UpdateTaskBoardCommand()
            {
                Id = 1,
                Name = "name_update",
                DefaultStatesId = 2
            };
        }



        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_crudDto, CancellationToken.None);

            var item = await _mockUow.Object.TaskBoardRepository.Get(_crudDto.Id, CancellationToken.None);
            Assert.That(item.Id == _crudDto.Id);
            Assert.That(item.Name == _crudDto.Name);
            Assert.That(item.DefaultStatesId == _crudDto.DefaultStatesId);


            var taskBoardStateList = await _mockUow.Object.TaskBoardStatesRepository.Get(p => p.TaskBoardId == _crudDto.Id && p.StatesId == _crudDto.DefaultStatesId, CancellationToken.None);
            Assert.That(taskBoardStateList.Any());
        }
        [Test]
        public async Task When_Name_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTaskBoardCommand()
            {
                Id = 1,
                Name = null,
                DefaultStatesId = 1
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_DefaultStateId_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTaskBoardCommand()
            {
                Id = 1,
                Name = "name",
                DefaultStatesId = 0
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_Id_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTaskBoardCommand()
            {
                Id = 0,
                Name = "name",
                DefaultStatesId = 2
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
