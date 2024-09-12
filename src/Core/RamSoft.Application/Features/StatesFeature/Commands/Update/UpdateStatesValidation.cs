using FluentValidation;
using RamSoft.Application.Contracts.Jira;

namespace RamSoft.Application.Features.StatesFeature.Commands.Update
{
    internal class UpdateStatesValidation : AbstractValidator<UpdateStatesCommand>
    {
        private readonly IStatesRepository _statesRepository;
        public UpdateStatesValidation(IStatesRepository statesRepository, CancellationToken cancellationToken)
        {
            _statesRepository = statesRepository;
            RuleFor(p => p)
                .MustAsync(async (entity, CancellationToken) => await Exists(entity.Id, cancellationToken))
                .WithMessage("Row not found.");

            RuleFor(p => p.Name)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 200 characters.");
        }
        private async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            return await _statesRepository.Exists(id, cancellationToken);
        }
    }
}
