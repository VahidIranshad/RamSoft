using FluentValidation;
using RamSoft.Application.Contracts.Jira;

namespace RamSoft.Application.Features.TaskBoardFeature.Commands.Delete
{
    internal class DeleteTaskBoardValidation : AbstractValidator<DeleteTaskBoardCommand>
    {
        private readonly ITaskBoardRepository _repository;
        public DeleteTaskBoardValidation(ITaskBoardRepository repository, CancellationToken cancellationToken)
        {
            _repository = repository;
            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity.Id, cancellationToken))
                .WithMessage("Row not found.");
        }
        private async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await _repository.Exists(id, cancellationToken);
        }
    }
}
