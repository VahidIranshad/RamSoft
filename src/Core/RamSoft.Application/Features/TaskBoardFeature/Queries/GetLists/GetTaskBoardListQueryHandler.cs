using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TaskBoardDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Queries.GetLists
{
    public class GetTaskBoardListQueryHandler : IQueryHandler<GetTaskBoardListQuery, IList<TaskBoardDto>>
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;


        public GetTaskBoardListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<TaskBoardDto>> Handle(GetTaskBoardListQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TaskBoardRepository.GetAll(cancellationToken);
            return _mapper.Map<IList<TaskBoardDto>>(result);
        }

    }
}
