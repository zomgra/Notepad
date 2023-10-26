using Notepad.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Notepad.Domain.ViewModel
{
    public class NoteViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel? Owner { get; set; }
        public ChunkViewModel? Chunk { get; set; } = null;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
