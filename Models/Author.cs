using System.ComponentModel.DataAnnotations;

namespace library_system.Models
{
    public class Author
    {
        [Key]
        [Required]// Explicit primary key
        public int Id { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string FirstName { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string SecondName { get; set; }

        public DateTime BirthDate { get; set; }
        public DateTime? DeadDate { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
