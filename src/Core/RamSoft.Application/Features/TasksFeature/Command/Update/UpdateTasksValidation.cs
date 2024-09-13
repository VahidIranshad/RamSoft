using FluentValidation;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.Application.Features.TasksFeature.Command.Update
{
    internal class UpdateTasksValidation : AbstractValidator<UpdateTasksCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateTasksValidation(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Name)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.")
                .MaximumLength(400).WithMessage("{PropertyName} must not exceed 400 characters.");

            RuleFor(p => p.Description)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Deadline)
                .GreaterThan(p => DateTime.Now).WithMessage("{PropertyName} must be greater than current date and time .");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await StatesExists(entity.StatesId, cancellationToken))
                .WithMessage("State is not valid.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await TaskBoardExists(entity.TaskBoardId, cancellationToken))
                .WithMessage("TaskBoard is not valid.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity.Id, cancellationToken))
                .WithMessage("Row not found.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await IsStateIdRelatedToTaskBoard(entity.TaskBoardId, entity.StatesId, cancellationToken))
                .WithMessage("TaskBoardId is not related to StatesId.");
        }
        private async Task<bool> StatesExists(int stateId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.StatesRepository.Exists(stateId, cancellationToken);
        }
        private async Task<bool> TaskBoardExists(int taskBoardId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TaskBoardRepository.Exists(taskBoardId, cancellationToken);
        }
        private async Task<bool> IsStateIdRelatedToTaskBoard(int taskBoardId, int statesId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TaskBoardStatesRepository.Exists(taskBoardId, statesId, cancellationToken);
        }
        private async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TasksRepository.Exists(id, cancellationToken);
        }

    }
}
