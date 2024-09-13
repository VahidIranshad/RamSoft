using FluentValidation;
using RamSoft.Application.Contracts.Base;

namespace RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Delete
{
    internal class DeleteTaskBoardStatesValidation : AbstractValidator<DeleteTaskBoardStatesCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteTaskBoardStatesValidation(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            _unitOfWork = unitOfWork;
            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity.Id, cancellationToken))
                .WithMessage("Row not found.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await IsDefault(entity.Id, cancellationToken))
                .WithMessage("This State is default of TaskBoard. You are not allowed to delete it until you change the default state of TaskBoard.");
        }
        private async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TaskBoardStatesRepository.Exists(id, cancellationToken);
        }
        private async Task<bool> IsDefault(int id, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.TaskBoardStatesRepository.Exists(id, cancellationToken))
            {
                return false;
            }
            var item = await _unitOfWork.TaskBoardStatesRepository.Get(id, cancellationToken);
            var taskBoard = await _unitOfWork.TaskBoardRepository.Get(item.TaskBoardId, cancellationToken);
            return !(taskBoard.DefaultStatesId == item.StatesId);
        }
    }
}
