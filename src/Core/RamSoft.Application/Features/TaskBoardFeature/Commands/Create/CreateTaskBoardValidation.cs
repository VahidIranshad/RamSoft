using FluentValidation;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Create
{
    internal class CreateTaskBoardValidation : AbstractValidator<CreateTaskBoardCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTaskBoardValidation(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Name)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await StatesExists(entity.DefaultStatesId, cancellationToken))
                .WithMessage("DefaultStates not found.");
        }
        private async Task<bool> StatesExists(int defaultStateID, CancellationToken cancellationToken)
        {
            return await _unitOfWork.StatesRepository.Exists(defaultStateID, cancellationToken);
        }
    }
}
