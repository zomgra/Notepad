using Notepad.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notepad.Domain.Entities
{
    public class Note : IEntity
    {
        /// <summary>
        /// Base .ctor
        /// </summary>
        public Note(string title, string description)
        {
            Id= Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Title = title;
            Description = description;
        }


        [Key]
        public Guid Id { get; set; }
        public User? Owner { get; set; }
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public NoteChunk? Chunk { get; set; } = null;
        [ForeignKey(nameof(Chunk))]
        public Guid? ChunkId { get; set; }

        [StringLength(maximumLength: 128, MinimumLength = 3)]
        public string Title { get; set; } = null!;
        [StringLength(maximumLength: 1000, MinimumLength = 10)]
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        /// <summary>
        /// .Ctor for EF Core
        /// </summary>
        public Note(Guid id, User owner, Guid ownerId, NoteChunk? chunk, Guid chunkId, string title, string description, DateTime createdAt)
        {
            Id = id;
            Owner = owner;
            OwnerId = ownerId;
            Chunk = chunk;
            ChunkId = chunkId;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}
