using MediatR;
using Microsoft.AspNetCore.Mvc;
using RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Create;
using RamSoft.Application.Features.TaskBoardStatesFeature.Commands.Delete;
using RamSoft.Application.Features.TaskBoardStatesFeature.Queries.GetListByTaskBoardID;
using System.Net.Mime;

namespace RamSoft.Api.Controllers.Jira
{
    [ApiController]
    [Route("api/jira/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    public class TaskBoardStatesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TaskBoardStatesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet("GetListByTaskBoardId")]
        public async Task<ActionResult> GetListByTaskBoardId([FromQuery] GetTaskBoardStatesListByTaskBoardIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateTaskBoardStatesCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteTaskBoardStatesCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
