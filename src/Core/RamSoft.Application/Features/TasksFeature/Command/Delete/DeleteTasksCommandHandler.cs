using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Command.Delete
{
    public class DeleteTasksCommandHandler : ICommandHandler<DeleteTasksCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteTasksCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTasksCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTasksValidation(_unitOfWork.TasksRepository, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {

                await _unitOfWork.TasksRepository.Delete(request.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
