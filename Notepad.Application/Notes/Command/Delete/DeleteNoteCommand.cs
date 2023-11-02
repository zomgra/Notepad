using MediatR;

namespace Notepad.Application.Notes.Command.Delete
{
    public record DeleteNoteCommand(Guid NoteId) : IRequest<Unit>;
}
