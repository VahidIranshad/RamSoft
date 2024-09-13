using FluentValidation;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Update
{
    internal class UpdateTaskBoardValidation : AbstractValidator<UpdateTaskBoardCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateTaskBoardValidation(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            _unitOfWork = unitOfWork;
            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity.Id, cancellationToken))
                .WithMessage("Row not found.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await DefaultStatesValidation(entity, cancellationToken))
                .WithMessage("DefaultStates is not valid.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await TaskBoardStatesValidation(entity, cancellationToken))
                .WithMessage("DefaultState is not exist in TaskBoardState. At first, you have to add this state to TaskBoardState.");

            RuleFor(p => p.Name)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
        private async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TaskBoardRepository.Exists(id, cancellationToken);
        }
        private async Task<bool> DefaultStatesValidation(UpdateTaskBoardCommand entity, CancellationToken cancellationToken)
        {

            return await _unitOfWork.StatesRepository.Exists(entity.DefaultStatesId, cancellationToken);
        }
        private async Task<bool> TaskBoardStatesValidation(UpdateTaskBoardCommand entity, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.TaskBoardStatesRepository.Get(p => p.TaskBoardId == entity.Id && p.StatesId == entity.DefaultStatesId, cancellationToken)).Any();
        }

    }
}
