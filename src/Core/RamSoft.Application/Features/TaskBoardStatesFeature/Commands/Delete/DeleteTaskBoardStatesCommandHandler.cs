using MediatR;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Exceptions;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Delete
{
    public class DeleteTaskBoardStatesCommandHandler : ICommandHandler<DeleteTaskBoardStatesCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskBoardStatesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteTaskBoardStatesCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTaskBoardStatesValidation(_unitOfWork, cancellationToken);
            var validationResult = await validator.ValidateAsync(request);


            if (validationResult.IsValid == false)
            {
                throw new CustomValidationException(validationResult);
            }
            else
            {

                await _unitOfWork.TaskBoardStatesRepository.Delete(request.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
