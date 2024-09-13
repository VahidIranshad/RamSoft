using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Create;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TaskBoardStatesFeature.Create
{
    public class CreateTaskBoardStatesCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateTaskBoardStatesCommand _crudDto;
        private readonly CreateTaskBoardStatesCommandHandler _handler;
        public CreateTaskBoardStatesCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateTaskBoardStatesCommandHandler(_mockUow.Object, _mapper);

            _crudDto = new CreateTaskBoardStatesCommand()
            {
                TaskBoardId = 1,
                StatesId = 3,
            };
        }



        [Test]
        public async Task Happy_Scenario()
        {
            var items = await _mockUow.Object.TaskBoardStatesRepository.GetAll(CancellationToken.None);
            int oldItemCount = items.Count;
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            items = await _mockUow.Object.TaskBoardStatesRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == oldItemCount + 1);

            var item = await _mockUow.Object.TaskBoardStatesRepository.Get(result, CancellationToken.None);
            Assert.That(item.Id == result);
            Assert.That(item.TaskBoardId == _crudDto.TaskBoardId);
            Assert.That(item.StatesId == _crudDto.StatesId);
        }

        [Test]
        public async Task When_StatesId_Is_Not_Valid_Throw_CustomValidationException()
        {
            var command = new CreateTaskBoardStatesCommand()
            {
                TaskBoardId = 1,
                StatesId = 4,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_TaskBoardId_Is_Not_Valid_Get_CustomValidationException()
        {
            var command = new CreateTaskBoardStatesCommand()
            {
                TaskBoardId = 2,
                StatesId = 3,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_Row_Is_Exists_Get_CustomValidationException()
        {
            var command = new CreateTaskBoardStatesCommand()
            {
                TaskBoardId = 1,
                StatesId = 1,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
