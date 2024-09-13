using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Delete
{
    public class DeleteTaskBoardCommandHandler : ICommandHandler<DeleteTaskBoardCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskBoardCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTaskBoardCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTaskBoardValidation(_unitOfWork.TaskBoardRepository, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {

                await _unitOfWork.TaskBoardRepository.Delete(request.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
