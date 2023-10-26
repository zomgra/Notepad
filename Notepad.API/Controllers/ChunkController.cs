using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notepad.API.Schemes;
using Notepad.Application.Chunks.Command.AddInChunk;
using Notepad.Application.Chunks.Command.Create.Create;
using Notepad.Application.Chunks.Queries.GetUserChunks;

namespace Notepad.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = nameof(CookieHaveUIDScheme))]
    public class ChunkController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChunkController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateChunkCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(GetUserChunksQuery query,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            if(result.Any())
                return Ok(result);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> AddInChunk(AddInChunkCommand command, 
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
