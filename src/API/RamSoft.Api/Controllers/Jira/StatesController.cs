using MediatR;
using Microsoft.AspNetCore.Mvc;
using RamSoft.Application.Features.StatesFeature.Commands.Create;
using RamSoft.Application.Features.StatesFeature.Commands.Delete;
using RamSoft.Application.Features.StatesFeature.Commands.Update;
using RamSoft.Application.Features.StatesFeature.Queries.GetLists;
using System.Net.Mime;

namespace RamSoft.Api.Controllers.Jira
{
    [ApiController]
    [Route("api/jira/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces("application/json")]
    public class StatesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public StatesController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
        }

        [HttpGet("GetList")]
        public async Task<ActionResult> GetList([FromQuery] GetStatesListQuery request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateStatesCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateStatesCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteStatesCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}
