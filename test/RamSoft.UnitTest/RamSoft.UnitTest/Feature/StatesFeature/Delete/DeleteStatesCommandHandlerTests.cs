using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.StatesFeature.Commands.Create;
using RamSoft.Application.Features.StatesFeature.Commands.Delete;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.StatesFeature.Delete
{
    public class DeleteStatesCommandHandlerTests
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly DeleteStatesCommand _crudDto;
        private readonly DeleteStatesCommandHandler _handler;
        private readonly DeleteStatesValidation _validator;
        public DeleteStatesCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new DeleteStatesValidation(_mockUow.Object.StatesRepository, default);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new DeleteStatesCommandHandler(_mockUow.Object);

            _crudDto = new DeleteStatesCommand()
            {
                Id = 1,
            };
        }

        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            var items = await _mockUow.Object.StatesRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == 0);
        }

        [Test]
        public async Task When_Id_Is_Not_Exist_Throw_Exception()
        {
            var command = new DeleteStatesCommand()
            {
                Id = 0,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
