using MediatR;
using Notepad.Domain.Entities;
using Notepad.Storage;

namespace Notepad.Application.Users.Command.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly NoteDbContext _context;

        public CreateUserCommandHandler(NoteDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User();
            var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            return user.Id;
        }
    }
}
