using AutoMapper;
using RamSoft.Application.Features.StatesFeature.Commands.Create;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStatesCommand, States>().ReverseMap();
        }
    }
}
