using AutoMapper;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.StatesFeature.Commands.Create
{
    public class CreateStatesCommandHandler : ICommandHandler<CreateStatesCommand, int>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStatesCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateStatesCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateStatesValidation();
            var validationResult = validator.Validate(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = _mapper.Map<States>(request);

                var result = await _unitOfWork.StatesRepository.Add(data);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return result.Id;
            }
        }
    }

}
