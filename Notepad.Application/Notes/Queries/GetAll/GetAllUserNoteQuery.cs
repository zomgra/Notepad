using MediatR;
using Notepad.Domain.ViewModel;

namespace Notepad.Application.Notes.Queries.GetAll
{
    public record GetAllUserNoteQuery() : IRequest<IEnumerable<NoteViewModel>>;
}
