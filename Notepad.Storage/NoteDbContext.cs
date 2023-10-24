using Microsoft.EntityFrameworkCore;
using Notepad.Domain.Entities;

namespace Notepad.Storage
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
        {

        }
        public DbSet<Note> Notes { get; set; }
    }
}
