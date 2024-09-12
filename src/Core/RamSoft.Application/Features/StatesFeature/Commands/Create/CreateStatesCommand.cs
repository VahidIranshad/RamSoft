using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.StatesFeature.Commands.Create
{
    public record CreateStatesCommand: ICommand<int>
    {
        public required string Name { get; set; }
    }
}
