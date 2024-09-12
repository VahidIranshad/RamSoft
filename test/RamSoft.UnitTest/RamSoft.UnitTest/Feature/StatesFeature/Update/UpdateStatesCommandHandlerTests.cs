using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.StatesFeature.Commands.Update;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.StatesFeature.Update
{
    public class UpdateStatesCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly UpdateStatesCommand _crudDto;
        private readonly UpdateStatesCommandHandler _handler;
        private readonly UpdateStatesValidation _validator;
        public UpdateStatesCommandHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();
            _validator = new UpdateStatesValidation(_mockUow.Object.StatesRepository, CancellationToken.None);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new UpdateStatesCommandHandler(_mockUow.Object, _mapper);

            _crudDto = new UpdateStatesCommand()
            {
                Id = 1,
                Name = "name",
            };
        }


        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_crudDto, CancellationToken.None);
            var item = await _mockUow.Object.StatesRepository.Get(_crudDto.Id, CancellationToken.None);
            Assert.That(item.Name == _crudDto.Name);
        }

        [Test]
        public async Task When_Name_Is_Not_Valid_Get_CustomValidationException()
        {
            var command = new UpdateStatesCommand()
            {
                Id= 1,
                Name = null,
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task When_Id_Is_Not_Valid_Get_CustomValidationException()
        {
            var command = new UpdateStatesCommand()
            {
                Id = 0,
                Name = "name",
            };
            Assert.ThrowsAsync<CustomValidationException>(
              async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
