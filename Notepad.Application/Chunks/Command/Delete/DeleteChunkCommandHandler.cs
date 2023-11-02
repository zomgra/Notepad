using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Common.Authentication;
using Notepad.Domain.Exceptions;
using Notepad.Storage;

namespace Notepad.Application.Chunks.Command.Delete
{
    public class DeleteChunkCommandHandler : IRequestHandler<DeleteChunkCommand, Guid>
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly NoteDbContext _context;

        public DeleteChunkCommandHandler(IIdentityProvider identityProvider,
            NoteDbContext context)
        {
            _identityProvider = identityProvider;
            _context = context;
        }

        public async Task<Guid> Handle(DeleteChunkCommand request, CancellationToken cancellationToken)
        {
            var chunk = await _context.Chunks.FirstOrDefaultAsync(x => x.Id == request.ChunkId, cancellationToken);
            if (chunk == null)
                throw new NotepadNotFoundException($"Chunk with {nameof(request.ChunkId)}: {request.ChunkId} not found");
            if (chunk.OwnerId != _identityProvider.UserId)
                throw new UnauthorizedAccessException();

            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _context.Chunks.Remove(chunk);
                await transaction.CommitAsync(cancellationToken);
                return chunk.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
