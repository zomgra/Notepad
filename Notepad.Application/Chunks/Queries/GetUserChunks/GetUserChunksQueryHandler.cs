using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Common.Authentication;
using Notepad.Domain.ViewModel;
using Notepad.Storage;

namespace Notepad.Application.Chunks.Queries.GetUserChunks
{
    public class GetUserChunksQueryHandler : IRequestHandler<GetUserChunksQuery, IEnumerable<ChunkViewModel>>
    {
        private readonly NoteDbContext _context;
        private readonly IMapper _mapper;
        private readonly IIdentityProvider _identityProvider;

        public GetUserChunksQueryHandler(NoteDbContext context, 
            IMapper mapper, 
            IIdentityProvider identityProvider)
        {
            _context = context;
            _mapper = mapper;
            _identityProvider = identityProvider;
        }

        public async Task<IEnumerable<ChunkViewModel>> Handle(GetUserChunksQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Chunks.Include(x=>x.Notes).Where(x => x.OwnerId == _identityProvider.UserId).ToListAsync(cancellationToken);
            var result = _mapper.Map<List<ChunkViewModel>>(list);
            return result;
        }
    }
}
