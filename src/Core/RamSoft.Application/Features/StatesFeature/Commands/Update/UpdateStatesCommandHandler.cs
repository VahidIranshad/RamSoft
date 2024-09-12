using AutoMapper;
using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.StatesFeature.Commands.Update
{
    public class UpdateStatesCommandHandler : ICommandHandler<UpdateStatesCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStatesCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateStatesCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStatesValidation(_unitOfWork.StatesRepository, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = _mapper.Map<States>(request);

                await _unitOfWork.StatesRepository.Update(data, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }

            return Unit.Value;
        }
    }
}
