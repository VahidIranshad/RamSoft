using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TasksDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Query.GetListByTaskBoardId
{
    public class GetTasksListByTaskBoardIdQueryHandler : IQueryHandler<GetTasksListByTaskBoardIdQuery, IList<TasksDto>>
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;


        public GetTasksListByTaskBoardIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<TasksDto>> Handle(GetTasksListByTaskBoardIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TasksRepository.GetListByTaskBoardId(request.TaskBoardId, cancellationToken);
            return _mapper.Map<IList<TasksDto>>(result);
        }

    }
}
