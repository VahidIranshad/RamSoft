using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.States;
using RamSoft.Application.Features.StatesFeature.Queries.GetLists;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;
using System.Reflection.Metadata;

namespace RamSoft.UnitTest.Feature.StatesFeature.GetList
{
    public class GetStatesListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly GetStatesListQuery _request;
        private readonly GetStatesListQueryHandler _handler;
        public GetStatesListQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetStatesListQueryHandler(_mockUow.Object, _mapper);

            _request = new GetStatesListQuery();
        }
        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_request, CancellationToken.None);

            var items = await _mockUow.Object.StatesRepository.GetAll(CancellationToken.None);
            var itemsDto = _mapper.Map<IList<StatesDto>>(result);
            CollectionAssert.AreEqual(itemsDto, result);
        }
    }

}
