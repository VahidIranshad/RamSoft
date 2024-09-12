using FluentValidation;
using RamSoft.Application.Contracts.Jira;

namespace RamSoft.Application.Features.StatesFeature.Commands.Delete
{
    internal class DeleteStatesValidation : AbstractValidator<DeleteStatesCommand>
    {
        private readonly IStatesRepository _statesRepository;
        public DeleteStatesValidation(IStatesRepository statesRepository, CancellationToken cancellationToken)
        {
            _statesRepository = statesRepository;
            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity.Id, cancellationToken))
                .WithMessage("Row not found.");
        }
        private async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await _statesRepository.Exists(id, cancellationToken);
        }
    }
}
