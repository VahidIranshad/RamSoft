using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.StatesFeature.Commands.Create;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.StatesFeature.Create
{
    public class CreateStatesCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly CreateStatesCommand _crudDto;
        private readonly CreateStatesCommandHandler _handler;
        private readonly CreateStatesValidation _validator;
        public CreateStatesCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new CreateStatesValidation();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateStatesCommandHandler(_mockUow.Object, _mapper);

            _crudDto = new CreateStatesCommand()
            {
                Name = "name",
            };
        }


        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            var items = await _mockUow.Object.StatesRepository.GetAll(CancellationToken.None);
            Assert.That(items.Count == 2);
        }

        [Test]
        public async Task When_Name_Is_Not_Valid_Get_CustomValidationException()
        {
            var command = new CreateStatesCommand()
            {
                Name = null,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
