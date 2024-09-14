using AutoMapper;
using Moq;
using NUnit.Framework.Legacy;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TaskBoardStatesDtos;
using RamSoft.Application.Features.TaskBoardStatesFeature.Queries.GetListByTaskBoardID;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TaskBoardStatesFeature.GetListByTaskBoardId
{
    public class GetTaskBoardStatesListByTaskBoardIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly GetTaskBoardStatesListByTaskBoardIdQuery _request;
        private readonly GetTaskBoardStatesListByTaskBoardIdQueryHandler _handler;
        public GetTaskBoardStatesListByTaskBoardIdQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetTaskBoardStatesListByTaskBoardIdQueryHandler(_mockUow.Object, _mapper);

            _request = new GetTaskBoardStatesListByTaskBoardIdQuery() { TaskBoardId = 1 };
        }
        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_request, CancellationToken.None);

            var items = await _mockUow.Object.TaskBoardStatesRepository.GetListByTaskBoardId(_request.TaskBoardId, CancellationToken.None);
            var itemsDto = _mapper.Map<IList<TaskBoardStateDto>>(result);
            CollectionAssert.AreEqual(itemsDto, result);
        }
    }
}
