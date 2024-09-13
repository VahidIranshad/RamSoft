using FluentValidation;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Create
{
    internal class CreateTaskBoardStatesValidation : AbstractValidator<CreateTaskBoardStatesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTaskBoardStatesValidation(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity, cancellationToken))
                .WithMessage("This state exists for this TaskBoard.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await StatesExists(entity.StatesId, cancellationToken))
                .WithMessage("states not found.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await TaskBoardExists(entity.TaskBoardId, cancellationToken))
                .WithMessage("TaskBoard not found.");
        }
        private async Task<bool> Exists(CreateTaskBoardStatesCommand entity, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.TaskBoardStatesRepository.Exists(entity.TaskBoardId, entity.StatesId, cancellationToken);
            return !result;
        }
        private async Task<bool> StatesExists(int stateID, CancellationToken cancellationToken)
        {
            return await _unitOfWork.StatesRepository.Exists(stateID, cancellationToken);
        }
        private async Task<bool> TaskBoardExists(int taskBoardId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TaskBoardRepository.Exists(taskBoardId, cancellationToken);
        }
    }
}
