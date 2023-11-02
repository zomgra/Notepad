using Notepad.Domain.Exceptions;
using Notepad.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notepad.Domain.Entities
{
    public class NoteChunk : IEntity
    {
        public NoteChunk(string name)
        {
            Name = name;
            CreateAt = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public User Owner { get; set; }
        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        /// <summary>
        /// .ctor for EF Core
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        public NoteChunk(Guid id, string name, User owner, DateTime upd, DateTime crt)
        {
            Id = id;
            Name = name;
            Owner = owner;
            CreateAt = crt;
            UpdateAt = upd;
        }
        public void Rename(string newName)
        {
            //if (string.IsNullOrEmpty(newName) || string.IsNullOrWhiteSpace(newName))
            //   throw new BadRequestException(nameof(newName) + " is null");  //WE HAVE VALIDATOR
            Name = newName;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
