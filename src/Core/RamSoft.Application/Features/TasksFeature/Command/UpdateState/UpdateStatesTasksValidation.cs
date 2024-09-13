using FluentValidation;
using RamSoft.Application.Contracts.Base;
using RamSoft.Application.Features.TasksFeature.Command.Update;
using RamSoft.Domain.Jira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamSoft.Application.Features.TasksFeature.Command.UpdateState
{
    internal class UpdateStatesTasksValidation : AbstractValidator<UpdateStatesTasksCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStatesTasksValidation(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await StatesExists(entity.StatesId, cancellationToken))
                .WithMessage("State is not valid.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity.Id, cancellationToken))
                .WithMessage("Row not found.");

            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await IsStateIdRelatedToTaskBoard(entity.Id, entity.StatesId, cancellationToken))
                .WithMessage("TaskBoardId is not related to StatesId.");
        }
        private async Task<bool> StatesExists(int stateId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.StatesRepository.Exists(stateId, cancellationToken);
        }
        private async Task<bool> IsStateIdRelatedToTaskBoard(int Id, int statesId, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.TasksRepository.Get(Id, cancellationToken);
            if (item == null)
            {
                return false;
            }
            return await _unitOfWork.TaskBoardStatesRepository.Exists(item.TaskBoardId, statesId, cancellationToken);
        }
        private async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TasksRepository.Exists(id, cancellationToken);
        }

    }
}
