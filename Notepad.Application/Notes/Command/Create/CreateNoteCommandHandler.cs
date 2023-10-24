using AutoMapper;
using MediatR;
using Notepad.Domain.Authentication;
using Notepad.Domain.Entities;
using Notepad.Domain.ViewModel;
using Notepad.Storage;

namespace Notepad.Application.Notes.Command.Create
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteViewModel>
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly NoteDbContext _noteContext;
        private readonly IMapper _mapper;

        public CreateNoteCommandHandler(IIdentityProvider identityProvider,
            NoteDbContext noteContext,
            IMapper mapper)
        {
            _identityProvider = identityProvider;
            _noteContext = noteContext;
            _mapper = mapper;
        }

        public async Task<NoteViewModel> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var id = _identityProvider.UserId;
            var note = new Note(request.title, request.description, id);
            _noteContext.Notes.Add(note);
            await _noteContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<NoteViewModel>(note);
        }
    }
}
