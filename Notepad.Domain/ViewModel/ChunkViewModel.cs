using Notepad.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notepad.Domain.ViewModel
{
    public class ChunkViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User Owner { get; set; }
        public ICollection<NoteViewModel> Notes { get; set; } = new List<NoteViewModel>();
    }
}
