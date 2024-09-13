using AutoMapper;
using Moq;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TasksDtos;
using RamSoft.Application.Features.TasksFeature.Query.GetById;
using RamSoft.Application.Profiles;
using RamSoft.UnitTest.Mock;

namespace RamSoft.UnitTest.Feature.TasksFeature.GetById
{
    public class GetTasksByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUow;
        private readonly GetTasksByIdQuery _request;
        private readonly GetTasksByIdQueryHandler _handler;
        public GetTasksByIdQueryHandlerTests()
        {
            _mockUow = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new GetTasksByIdQueryHandler(_mockUow.Object, _mapper);

            _request = new GetTasksByIdQuery() { Id = 1 };
        }
        [Test]
        public async Task Happy_Scenario()
        {
            var result = await _handler.Handle(_request, CancellationToken.None);

            var item = await _mockUow.Object.TasksRepository.Get(_request.Id, CancellationToken.None);
            var itemsDto = _mapper.Map<TasksDto>(result);
            Assert.That(result.Equals(itemsDto));
        }
    }
}
