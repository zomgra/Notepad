using AutoMapper;
using MediatR;
using Notepad.Application.Common.Authentication;
using Notepad.Application.Common.Repositories;
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
        private readonly UserRepository _userRepository;

        public CreateNoteCommandHandler(IIdentityProvider identityProvider,
            NoteDbContext noteContext,
            IMapper mapper,
            UserRepository userRepository)
        {
            _identityProvider = identityProvider;
            _noteContext = noteContext;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<NoteViewModel> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var id = _identityProvider.UserId;
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new UnauthorizedAccessException();

            var note = new Note(request.title, request.description);
            _noteContext.Users.Attach(user);
            await _noteContext.AddAsync(note, cancellationToken);
            note.Owner = user;

            await _noteContext.SaveChangesAsync(cancellationToken);
            //await _noteContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<NoteViewModel>(note);
        }
    }
}
