using AutoMapper;
using Moq;
using NUnit.Framework.Legacy;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TaskBoardDtos;
using RamSoft.Application.Features.TaskBoardFeature.Queries.GetLists;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TaskBoardFeature.GetList
{
    public class GetTaskBoardQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly GetTaskBoardListQuery _request;
        private readonly GetTaskBoardListQueryHandler _handler;
        public GetTaskBoardQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetTaskBoardListQueryHandler(_mockUow.Object, _mapper);

            _request = new GetTaskBoardListQuery();
        }
        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_request, CancellationToken.None);

            var items = await _mockUow.Object.TaskBoardRepository.GetAll(CancellationToken.None);
            var itemsDto = _mapper.Map<IList<TaskBoardDto>>(result);
            CollectionAssert.AreEqual(itemsDto, result);
        }
    }
}
