using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TasksFeature.Command.Delete;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TasksFeature.Delete
{
    public class DeleteTasksCommandHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly DeleteTasksCommand _crudDto;
        private readonly DeleteTasksCommandHandler _handler;
        private readonly DeleteTasksValidation _validator;
        public DeleteTasksCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new DeleteTasksValidation(_mockUow.Object.TasksRepository, default);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new DeleteTasksCommandHandler(_mockUow.Object);

            _crudDto = new DeleteTasksCommand()
            {
                Id = 1,
            };
        }

        [Test]
        public async Task Happy_Scenario()
        {
            var items = await _mockUow.Object.TasksRepository.GetAll(CancellationToken.None);
            var oldItemCounts = items.Count;
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            items = await _mockUow.Object.TasksRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == oldItemCounts - 1);
        }

        [Test]
        public async Task When_Id_Is_Not_Exist_Throw_Exception()
        {
            var command = new DeleteTasksCommand()
            {
                Id = 0,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
