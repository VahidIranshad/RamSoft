using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TasksFeature.Command.Create;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TasksFeature.Create
{
    public class CreateTasksCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateTasksCommandHandler _handler;
        public CreateTasksCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateTasksCommandHandler(_mockUow.Object, _mapper);

        }



        [Test]
        public async Task Happy_Scenario()
        {
            var _crudDto = new CreateTasksCommand()
            {
                Name = "Test",
                Description = "Test Description",
                TaskBoardId = 1,
                StatesId = 1,
            };
            var items = await _mockUow.Object.TasksRepository.GetAll(CancellationToken.None);
            int oldItemCount = items.Count;
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            items = await _mockUow.Object.TasksRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == oldItemCount + 1);

            var item = await _mockUow.Object.TasksRepository.Get(result, CancellationToken.None);
            Assert.That(item.Id == result);
            Assert.That(item.TaskBoardId == _crudDto.TaskBoardId);
            Assert.That(item.StatesId == _crudDto.StatesId);
            Assert.That(item.Name == _crudDto.Name);
            Assert.That(item.Description == _crudDto.Description);
        }

        [Test]
        public async Task When_StatesId_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new CreateTasksCommand()
            {
                Name = "Test",
                Description = "Test Description",
                TaskBoardId = 1,
                StatesId = 4,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_TaskBoardId_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new CreateTasksCommand()
            {
                Name = "Test",
                Description = "Test Description",
                TaskBoardId = 2,
                StatesId = 3,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_Name_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new CreateTasksCommand()
            {
                Name = "",
                Description = "Test Description",
                TaskBoardId = 1,
                StatesId = 1,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_Description_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new CreateTasksCommand()
            {
                Name = "Test",
                Description = "",
                TaskBoardId = 1,
                StatesId = 1,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
