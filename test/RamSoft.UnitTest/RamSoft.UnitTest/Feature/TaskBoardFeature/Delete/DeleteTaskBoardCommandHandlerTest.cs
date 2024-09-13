using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Delete;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TaskBoardFeature.Delete
{
    public class DeleteTaskBoardCommandHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly DeleteTaskBoardCommand _crudDto;
        private readonly DeleteTaskBoardCommandHandler _handler;
        private readonly DeleteTaskBoardValidation _validator;
        public DeleteTaskBoardCommandHandlerTest()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new DeleteTaskBoardValidation(_mockUow.Object.TaskBoardRepository, default);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new DeleteTaskBoardCommandHandler(_mockUow.Object);

            _crudDto = new DeleteTaskBoardCommand()
            {
                Id = 1,
            };
        }

        [Test]
        public async Task Happy_Scenario()
        {
            var items = await _mockUow.Object.TaskBoardRepository.GetAll(CancellationToken.None);
            var oldItemCounts = items.Count;
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            items = await _mockUow.Object.TaskBoardRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == oldItemCounts - 1);
        }

        [Test]
        public async Task When_Id_Is_Not_Exist_Throw_Exception()
        {
            var command = new DeleteTaskBoardCommand()
            {
                Id = 0,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
