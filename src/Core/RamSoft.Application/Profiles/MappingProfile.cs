using AutoMapper;
using RamSoft.Application.Dtos.Jira.StatesDtos;
using RamSoft.Application.Dtos.Jira.TaskBoardDtos;
using RamSoft.Application.Dtos.Jira.TaskBoardStatesDtos;
using RamSoft.Application.Dtos.Jira.TasksDtos;
using RamSoft.Application.Features.StatesFeature.Commands.Create;
using RamSoft.Application.Features.StatesFeature.Commands.Update;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Create;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Update;
using RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Create;
using RamSoft.Application.Features.TasksFeature.Command.Create;
using RamSoft.Application.Features.TasksFeature.Command.Update;
using RamSoft.Domain.Jira;

namespace RamSoft.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateStatesCommand, States>().ReverseMap();
            CreateMap<UpdateStatesCommand, States>().ReverseMap();
            CreateMap<StatesDto, States>().ReverseMap();


            CreateMap<CreateTaskBoardCommand, TaskBoard>().ReverseMap();
            CreateMap<UpdateTaskBoardCommand, TaskBoard>().ReverseMap();
            CreateMap<TaskBoard, TaskBoardDto>()
                .ForMember(p => p.DefaultStatesName, o => o.MapFrom(s => s.DefaultStates.Name));

            CreateMap<CreateTaskBoardStatesCommand, TaskBoardStates>().ReverseMap();
            CreateMap<TaskBoardStates, TaskBoardStateDto>()
                .ForMember(p => p.StatesName, o => o.MapFrom(s => s.States.Name));


            CreateMap<CreateTasksCommand, Tasks>().ReverseMap();
            CreateMap<UpdateTasksCommand, Tasks>().ReverseMap();
            CreateMap<Tasks, TasksDto>()
                .ForMember(p => p.StatesName, o => o.MapFrom(s => s.States.Name))
                .ForMember(p => p.TaskBoardName, o => o.MapFrom(s => s.TaskBoard.Name));

        }
    }
}
