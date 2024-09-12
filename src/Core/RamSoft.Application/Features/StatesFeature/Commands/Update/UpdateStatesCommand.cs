using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.StatesFeature.Commands.Update
{
    public record UpdateStatesCommand : ICommand
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
