using RamSoft.Application.Dtos.Jira.TasksDtos;
using RamSoft.Application.Features.Base;

namespace RamSoft.Application.Features.TasksFeature.Query.GetById
{
    public record GetTasksByIdQuery : IQuery<TasksDto>
    {
        public int Id { get; set; }
    }
}
