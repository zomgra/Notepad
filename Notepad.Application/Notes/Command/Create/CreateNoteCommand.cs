using MediatR;
using Notepad.Domain.ViewModel;

namespace Notepad.Application.Notes.Command.Create
{
    public record CreateNoteCommand(string Title, string Description) : IRequest<NoteViewModel>;
    
}
