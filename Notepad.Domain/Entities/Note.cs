using Notepad.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Notepad.Domain.Entities
{
    public class Note : IEntity
    {
        /// <summary>
        /// Base .ctor
        /// </summary>
        public Note(string title, string description, Guid userId)
        {
            CreatedAt = DateTime.UtcNow;
            Title = title;
            Description = description;
            UserId = userId;
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [StringLength(maximumLength: 128, MinimumLength = 3)]
        public string Title { get; set; } = null!;
        [StringLength(maximumLength: 1000, MinimumLength = 10)]
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        /// <summary>
        /// .Ctor for EF Core
        /// </summary>
        public Note(Guid userId,
            string title,
            string description,
            DateTime createdAt,
            DateTime? updatedAt)
        {
            UserId = userId;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
