using AutoMapper;
using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.TasksFeature.Command.UpdateState
{
    public class UpdateStatesTasksCommandHandler : ICommandHandler<UpdateStatesTasksCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStatesTasksCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateStatesTasksCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateStatesTasksValidation(_unitOfWork, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = await _unitOfWork.TasksRepository.Get(request.Id, cancellationToken);
                data.StatesId = request.StatesId;

                await _unitOfWork.TasksRepository.Update(data, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }

            return Unit.Value;
        }
    }
}
