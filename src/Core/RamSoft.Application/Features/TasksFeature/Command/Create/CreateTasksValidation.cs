using FluentValidation;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.Application.Features.TasksFeature.Command.Create
{
    internal class CreateTasksValidation : AbstractValidator<CreateTasksCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTasksValidation(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Name)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.")
                .MaximumLength(400).WithMessage("{PropertyName} must not exceed 400 characters.");

            RuleFor(p => p.Description)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Deadline)
                .LessThan(p => DateTime.Now).WithMessage("{PropertyName} must be greater than current date and time .");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await StatesExists(entity.StatesId, entity.TaskBoardId, cancellationToken))
                .WithMessage("State is not valid.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await TaskBoardExists(entity.TaskBoardId, cancellationToken))
                .WithMessage("TaskBoard is not valid.");
        }
        private async Task<bool> StatesExists(int stateId, int taskBoardId, CancellationToken cancellationToken)
        {
            var result =  await _unitOfWork.StatesRepository.Exists(stateId, cancellationToken);
            if (result == false)
            {
                return result;
            }
            return await _unitOfWork.TaskBoardStatesRepository.Exists(taskBoardId, stateId, cancellationToken);
        }
        private async Task<bool> TaskBoardExists(int taskBoardId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TaskBoardRepository.Exists(taskBoardId, cancellationToken);
        }
    }
}
