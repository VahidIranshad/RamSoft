using AutoMapper;
using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Update
{
    public class UpdateTaskBoardCommandHandler : ICommandHandler<UpdateTaskBoardCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskBoardCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateTaskBoardCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTaskBoardValidation(_unitOfWork, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = _mapper.Map<TaskBoard>(request);

                await _unitOfWork.TaskBoardRepository.Update(data, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }

            return Unit.Value;
        }
    }
}
