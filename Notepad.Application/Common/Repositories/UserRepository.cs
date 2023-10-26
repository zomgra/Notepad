using Notepad.Domain.Entities;
using Notepad.Storage;

namespace Notepad.Application.Common.Repositories
{
    public class UserRepository
    {
        private readonly NoteDbContext _context;

        public UserRepository(NoteDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return null;
            return user;
        }
    }
}
