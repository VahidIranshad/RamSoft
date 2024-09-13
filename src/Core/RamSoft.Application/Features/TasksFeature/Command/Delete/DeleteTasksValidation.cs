using FluentValidation;
using RamSoft.Application.Contracts.Jira;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Delete;

namespace RamSoft.Application.Features.TasksFeature.Command.Delete
{
    internal class DeleteTasksValidation : AbstractValidator<DeleteTasksCommand>
    {
        private readonly ITasksRepository _repository;
        public DeleteTasksValidation(ITasksRepository repository, CancellationToken cancellationToken)
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
