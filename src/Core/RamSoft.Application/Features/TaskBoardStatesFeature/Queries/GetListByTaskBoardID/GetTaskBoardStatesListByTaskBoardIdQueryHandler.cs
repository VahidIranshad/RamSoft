using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TaskBoardStatesDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Queries.GetListByTaskBoardID
{
    public class GetTaskBoardStatesListByTaskBoardIdQueryHandler : IQueryHandler<GetTaskBoardStatesListByTaskBoardIdQuery, IList<TaskBoardStateDto>>
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;


        public GetTaskBoardStatesListByTaskBoardIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<TaskBoardStateDto>> Handle(GetTaskBoardStatesListByTaskBoardIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TaskBoardStatesRepository.GetListByTaskBoardId(request.TaskBoardId, cancellationToken);
            return _mapper.Map<IList<TaskBoardStateDto>>(result);
        }

    }
}
