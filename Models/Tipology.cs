using System.ComponentModel.DataAnnotations;

namespace library_system.Models
{
    public class Tipology
    {
        [Key]  // Explicit primary key
        [Required]
        public int Id { get; set; }

        [Required] // NOT NULL
        [MaxLength(300)] // nvarchar(100)
        public string Description { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
