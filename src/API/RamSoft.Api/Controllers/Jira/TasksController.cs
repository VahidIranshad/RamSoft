using MediatR;
using Microsoft.AspNetCore.Mvc;
using RamSoft.Application.Features.TasksFeature.Command.Create;
using RamSoft.Application.Features.TasksFeature.Command.Delete;
using RamSoft.Application.Features.TasksFeature.Command.Update;
using RamSoft.Application.Features.TasksFeature.Command.UpdateState;
using RamSoft.Application.Features.TasksFeature.Query.GetById;
using RamSoft.Application.Features.TasksFeature.Query.GetListByTaskBoardId;
using System.Net.Mime;

namespace RamSoft.Api.Controllers.Jira
{
    [ApiController]
    [Route("api/jira/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    public class TasksController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TasksController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet("GetTasksById")]
        public async Task<ActionResult> GetTasksById([FromQuery] GetTasksByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("GetListByTaskBoardID")]
        public async Task<ActionResult> GetListByTaskBoardID([FromQuery] GetTasksListByTaskBoardIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateTasksCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateTasksCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("PutState")]
        public async Task<ActionResult> PutState([FromBody] UpdateStatesTasksCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteTasksCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
