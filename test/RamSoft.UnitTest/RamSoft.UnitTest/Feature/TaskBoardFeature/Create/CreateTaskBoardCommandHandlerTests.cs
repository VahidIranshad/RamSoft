using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.StatesDtos;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.StatesFeature.Commands.Create;
using RamSoft.Application.Features.StatesFeature.Commands.Update;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Create;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Update;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TaskBoardFeature.Create
{
    public class CreateTaskBoardCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateTaskBoardCommand _crudDto;
        private readonly CreateTaskBoardCommandHandler _handler;
        private readonly CreateTaskBoardValidation _validator;
        public CreateTaskBoardCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new CreateTaskBoardValidation(_mockUow.Object, CancellationToken.None);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateTaskBoardCommandHandler(_mockUow.Object, _mapper);

            _crudDto = new CreateTaskBoardCommand()
            {
                Name = "name",
                DefaultStatesId = 1
            };
        }



        [Test]
        public async Task Happy_Scenario()
        {
            var items = await _mockUow.Object.TaskBoardRepository.GetAll(CancellationToken.None);
            int oldItemCount = items.Count;
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            items = await _mockUow.Object.TaskBoardRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == oldItemCount + 1);

            var item = await _mockUow.Object.TaskBoardRepository.Get(result, CancellationToken.None);
            Assert.That(item.Id == result);
            Assert.That(item.Name == _crudDto.Name);
            Assert.That(item.DefaultStatesId == _crudDto.DefaultStatesId);


            var taskBoardStateList = await _mockUow.Object.TaskBoardStatesRepository.Get(p => p.TaskBoardId == result && p.StatesId == _crudDto.DefaultStatesId, CancellationToken.None);
            Assert.That(taskBoardStateList.Any());
        }
        [Test]
        public async Task When_Name_Is_Not_Valid_Get_CustomValidationException()
        {
            var command = new CreateTaskBoardCommand()
            {
                Name = null,
                DefaultStatesId = 1
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_DefaultStateId_Is_Not_Valid_Get_CustomValidationException()
        {
            var command = new CreateTaskBoardCommand()
            {
                Name = "name",
                DefaultStatesId = 0
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
