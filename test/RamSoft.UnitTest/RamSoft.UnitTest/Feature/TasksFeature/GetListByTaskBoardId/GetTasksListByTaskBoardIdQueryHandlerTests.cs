using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TasksDtos;
using RamSoft.Application.Features.TasksFeature.Query.GetListByTaskBoardId;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TasksFeature.GetListByTaskBoardId
{
    public class GetTasksListByTaskBoardIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly GetTasksListByTaskBoardIdQuery _request;
        private readonly GetTasksListByTaskBoardIdQueryHandler _handler;
        public GetTasksListByTaskBoardIdQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetTasksListByTaskBoardIdQueryHandler(_mockUow.Object, _mapper);

            _request = new GetTasksListByTaskBoardIdQuery() { TaskBoardId = 1 };
        }
        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_request, CancellationToken.None);

            var items = await _mockUow.Object.TasksRepository.GetListByTaskBoardId(_request.TaskBoardId, CancellationToken.None);
            var itemsDto = _mapper.Map<IList<TasksDto>>(result);
            CollectionAssert.AreEqual(itemsDto, result);
        }
    }
}
