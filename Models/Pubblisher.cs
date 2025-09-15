using System.ComponentModel.DataAnnotations;

namespace library_system.Models
{
    public class Pubblisher
    {
        [Key]  // Explicit primary key
        [Required]
        public int Id { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string Company { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
