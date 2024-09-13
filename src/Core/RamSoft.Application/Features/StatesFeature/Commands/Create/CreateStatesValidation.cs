using FluentValidation;

namespace RamSoft.Application.Features.StatesFeature.Commands.Create
{
    internal class CreateStatesValidation : AbstractValidator<CreateStatesCommand>
    {
        public CreateStatesValidation()
        {
            RuleFor(p => p.Name)
                .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
