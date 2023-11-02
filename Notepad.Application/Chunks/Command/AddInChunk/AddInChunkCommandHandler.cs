using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Common.Authentication;
using Notepad.Storage;

namespace Notepad.Application.Chunks.Command.AddInChunk
{
    public class AddInChunkCommandHandler : IRequestHandler<AddInChunkCommand, bool>
    {
        private readonly NoteDbContext _context;
        private readonly IIdentityProvider _identityProvider;

        public AddInChunkCommandHandler(NoteDbContext context,
            IIdentityProvider identityProvider)
        {
            _context = context;
            _identityProvider = identityProvider;
        }

        public async Task<bool> Handle(AddInChunkCommand request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes.Include(x=>x.Chunk).FirstOrDefaultAsync(x => x.Id == request.NoteId, cancellationToken);
            if (note == null)
                throw new Exception("Note not found");/// TODO: Change to self exception;
            if (note.OwnerId != _identityProvider.UserId)
                throw new UnauthorizedAccessException();

            var chunk = await _context.Chunks.Include(x=>x.Notes).FirstOrDefaultAsync(x => x.Id == request.ChunkId, cancellationToken);
            if (chunk == null)
                throw new Exception("Chunk not found");/// TODO: Change to self exception;
            if (chunk.OwnerId != _identityProvider.UserId)
                throw new UnauthorizedAccessException();

            chunk.Notes.Add(note);
            note.ChunkId = chunk.Id;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
