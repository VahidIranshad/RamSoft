using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Dtos.Jira.StatesDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.StatesFeature.Queries.GetLists
{
    public class GetStatesListQueryHandler : IQueryHandler<GetStatesListQuery, IList<StatesDto>>
    {

        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;


        public GetStatesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IList<StatesDto>> Handle(GetStatesListQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.StatesRepository.GetAll(cancellationToken);
            return _mapper.Map<IList<StatesDto>>(result);
        }

    }
}
