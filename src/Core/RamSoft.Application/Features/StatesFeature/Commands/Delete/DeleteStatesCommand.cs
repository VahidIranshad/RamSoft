using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.StatesFeature.Commands.Delete
{
    public record DeleteStatesCommand : ICommand
    {
        public int Id { get; set; }
    }
}
