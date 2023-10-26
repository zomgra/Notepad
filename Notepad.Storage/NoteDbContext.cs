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
        public DbSet<User> Users { get; set; }
        public DbSet<NoteChunk> Chunks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Note>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<NoteChunk>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Notes)
                .WithOne(x => x.Owner);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Chunks)
                .WithOne(x => x.Owner);

            modelBuilder.Entity<NoteChunk>()
               .HasOne(x => x.Owner)
               .WithMany(x => x.Chunks)
               .HasForeignKey(x=>x.OwnerId);
            modelBuilder.Entity<Note>()
               .HasOne(x => x.Owner)
               .WithMany(x => x.Notes)
               .HasForeignKey(x=>x.OwnerId);
        }
    }
}
