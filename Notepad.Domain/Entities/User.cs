using Notepad.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notepad.Domain.Entities
{
    public class User : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [InverseProperty(nameof(Note.Owner))]
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        [InverseProperty(nameof(NoteChunk.Owner))]
        public ICollection<NoteChunk>? Chunks { get; set; } = new List<NoteChunk>();
    }
}
