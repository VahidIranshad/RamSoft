using MediatR;
using Microsoft.AspNetCore.Mvc;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Create;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Delete;
using RamSoft.Application.Features.TaskBoardFeature.Commands.Update;
using RamSoft.Application.Features.TaskBoardFeature.Queries.GetLists;
using System.Net.Mime;

namespace RamSoft.Api.Controllers.Jira
{
    [ApiController]
    [Route("api/jira/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    public class TaskBoardController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TaskBoardController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult> GetList([FromQuery] GetTaskBoardListQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateTaskBoardCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateTaskBoardCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteTaskBoardCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
