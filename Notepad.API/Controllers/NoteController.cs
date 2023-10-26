using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notepad.API.Schemes;
using Notepad.Application.Notes.Command.Create;
using Notepad.Application.Notes.Command.Delete;
using Notepad.Application.Notes.Queries.GetAll;

namespace Notepad.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = nameof(CookieHaveUIDScheme))]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteCommand command,
            CancellationToken cancellationToken)
        {
            var note = await _mediator.Send(command, cancellationToken);
            return Ok(note);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var notes = await _mediator.Send(new GetAllUserNoteQuery(), cancellationToken);
            if (notes == null)
                return NoContent();
            return Ok(notes);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteNoteCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();   
        }
    }
}
