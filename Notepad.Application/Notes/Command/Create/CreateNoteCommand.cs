using MediatR;
using Notepad.Domain.ViewModel;

namespace Notepad.Application.Notes.Command.Create
{
    public record CreateNoteCommand(string title, string description) : IRequest<NoteViewModel>;
    
}
