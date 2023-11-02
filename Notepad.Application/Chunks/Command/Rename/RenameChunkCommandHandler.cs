using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Common.Authentication;
using Notepad.Domain.Exceptions;
using Notepad.Storage;

namespace Notepad.Application.Chunks.Command.Rename
{
    public class RenameChunkCommandHandler : IRequestHandler<RenameChunkCommand, Unit>
    {
        private readonly NoteDbContext _context;
        private readonly IIdentityProvider _identityProvider;

        public RenameChunkCommandHandler(NoteDbContext context,
            IIdentityProvider identityProvider)
        {
            _context = context;
            _identityProvider = identityProvider;
        }

        public async Task<Unit> Handle(RenameChunkCommand request, CancellationToken cancellationToken)
        {
            var chunk = await _context.Chunks.FirstOrDefaultAsync(x => x.Id == request.ChunkId, cancellationToken);
            if (chunk == null)
                throw new NotepadNotFoundException($"Chunk with id: {request.ChunkId} not found");
            if (_identityProvider.UserId != chunk.OwnerId)
                throw new UnauthorizedAccessException();

            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                chunk.Rename(request.NewName);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
