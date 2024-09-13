using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TasksFeature.Command.Update;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;


namespace RamSoft.UnitTest.Feature.TasksFeature.Update
{
    public class UpdateTasksCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly UpdateTasksCommandHandler _handler;
        private readonly UpdateTasksValidation _validator;
        public UpdateTasksCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new UpdateTasksValidation(_mockUow.Object, CancellationToken.None);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateTasksCommandHandler(_mockUow.Object, _mapper);

        }



        [Test]
        public async Task Happy_Scenario()
        {
            var _crudDto = new UpdateTasksCommand()
            {
                Id = 1,
                Name = "name_update",
                Description = "new Desc",
                StatesId = 2,
                TaskBoardId = 1,
                Deadline = DateTime.Now.AddDays(1),
            };
            var result = await _handler.Handle(_crudDto, CancellationToken.None);

            var item = await _mockUow.Object.TasksRepository.Get(_crudDto.Id, CancellationToken.None);
            Assert.That(item.Id == _crudDto.Id);
            Assert.That(item.Name == _crudDto.Name);
            Assert.That(item.Description == _crudDto.Description);
            Assert.That(item.TaskBoardId == _crudDto.TaskBoardId);
            Assert.That(item.StatesId == _crudDto.StatesId);
        }
        [Test]
        public async Task When_Name_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTasksCommand()
            {
                Id = 1,
                Name = "",
                Description = "new Desc",
                StatesId = 2,
                TaskBoardId = 1,
                Deadline = DateTime.Now.AddDays(1),
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
        [Test]
        public async Task When_Description_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTasksCommand()
            {
                Id = 1,
                Name = "new name update",
                Description = "",
                StatesId = 2,
                TaskBoardId = 1,
                Deadline = DateTime.Now.AddDays(1),
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
        [Test]
        public async Task When_DeadLine_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTasksCommand()
            {
                Id = 1,
                Name = "new name update",
                Description = "",
                StatesId = 2,
                TaskBoardId = 1,
                Deadline = DateTime.Now.AddDays(-1),
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_StatesId_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTasksCommand()
            {
                Id = 1,
                Name = "name_update",
                Description = "new Desc",
                StatesId = 0,
                TaskBoardId = 2,
                Deadline = DateTime.Now.AddDays(1),
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_Id_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTasksCommand()
            {
                Id = 0,
                Name = "name_update",
                Description = "new Desc",
                StatesId = 2,
                TaskBoardId = 1,
                Deadline = DateTime.Now.AddDays(1),
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_TaskBoardId_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new UpdateTasksCommand()
            {
                Id = 1,
                Name = "name_update",
                Description = "new Desc",
                StatesId = 2,
                TaskBoardId = 0,
                Deadline = DateTime.Now.AddDays(1),
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_TaskBoardId_Is_Not_Related_To_StatesId_Throw_CustomValidationException()
        {
            var command = new UpdateTasksCommand()
            {
                Id = 1,
                Name = "name_update",
                Description = "new Desc",
                StatesId = 4,
                TaskBoardId = 1,
                Deadline = DateTime.Now.AddDays(1),
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
