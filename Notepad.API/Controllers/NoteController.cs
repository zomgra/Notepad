using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notepad.Application.Notes.Command.Create;

namespace Notepad.API.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
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
    }
}
