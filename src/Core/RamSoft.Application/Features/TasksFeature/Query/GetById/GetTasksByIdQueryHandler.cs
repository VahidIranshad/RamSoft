using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.TasksDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Query.GetById
{
    public class GetTasksByIdQueryHandler : IQueryHandler<GetTasksByIdQuery, TasksDto>
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;


        public GetTasksByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TasksDto> Handle(GetTasksByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TasksRepository.Get(request.Id, cancellationToken);
            return _mapper.Map<TasksDto>(result);
        }

    }
}
