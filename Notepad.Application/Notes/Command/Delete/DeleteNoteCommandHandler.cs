using MediatR;
using Microsoft.EntityFrameworkCore;
using Notepad.Application.Common.Authentication;
using Notepad.Storage;
using System.Data.Common;

namespace Notepad.Application.Notes.Command.Delete
{
    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Unit>
    {
        private readonly NoteDbContext _context;
        private readonly IIdentityProvider _identityProvider;

        public DeleteNoteCommandHandler(NoteDbContext context,
            IIdentityProvider identityProvider)
        {
            _context = context;
            _identityProvider = identityProvider;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes.Include(x=>x.Owner).FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (note == null)
                throw new Exception("Note not found"); /// TODO: Change to self exceptions;

            if (note.OwnerId != _identityProvider.UserId)
                throw new UnauthorizedAccessException();

            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }
            return Unit.Value;
        }
    }
}
