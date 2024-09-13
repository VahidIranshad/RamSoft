using AutoMapper;
using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Features.TasksFeature.Command.Update
{
    public class UpdateTasksCommandHandler : ICommandHandler<UpdateTasksCommand, Unit>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTasksCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateTasksCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTasksValidation(_unitOfWork, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {
                var data = _mapper.Map<Tasks>(request);

                await _unitOfWork.TasksRepository.Update(data, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }

            return Unit.Value;
        }
    }
}
