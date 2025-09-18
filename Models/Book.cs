using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_system.Models
{
    public class Book
    {
        [Key] // Explicit primary key
        [Required]
        public int Id { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string Title { get; set; }

        [MaxLength(300)] // nvarchar(100)
        public string Description { get; set; }

        [ForeignKey("Tipology_Id")]
        [Required]
        public int TipologyId { get; set; }
        public Tipology Tipology { get; set; }
        public string ISBN { get; set; }
        [Required]
        [ForeignKey("Author_Id")]
        public int AuthorId { get; set; }
        public Author ?Authors { get; set; }

        [ForeignKey("Pubblisher_Id")]
        [Required]
        public int PubblisherId { get; set; }
        public Pubblisher Pubblisher { get; set; }
        public DateTime PubblicDate { get; set; }
        public bool IsBorrowed { get; set; } = false; 
        public ICollection<Borrow> borrows { get; set; }


    }
}
