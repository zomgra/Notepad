using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Common.Authentication;
using Notepad.Domain.ViewModel;
using Notepad.Storage;

namespace Notepad.Application.Notes.Queries.GetAll
{
    public class GetAllUserNoteQueryHandler : IRequestHandler<GetAllUserNoteQuery, IEnumerable<NoteViewModel>>
    {
        private readonly NoteDbContext _context;
        private readonly IIdentityProvider _identityProvider;
        private readonly IMapper _mapper;

        public GetAllUserNoteQueryHandler(NoteDbContext context,
            IIdentityProvider identityProvider,
            IMapper mapper)
        {
            _context = context;
            _identityProvider = identityProvider;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteViewModel>> Handle(GetAllUserNoteQuery request, CancellationToken cancellationToken)
        {
            var userId = _identityProvider.UserId;

            var notes = await _context.Notes.Include(x=>x.Chunk).Where(x => x.OwnerId == userId).ToListAsync(cancellationToken);
            var vmnotes = _mapper.Map<List<NoteViewModel>>(notes);
            return vmnotes;
        }
    }
}
