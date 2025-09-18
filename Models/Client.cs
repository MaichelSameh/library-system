using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace library_system.Models
{
    public class Client
    {
        [Key]  // Explicit primary key
        [Required]
        public int Id { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string FirstName { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // nvarchar(100)
        public string SecondName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
        public string FiscalCode { get; set; }

        [MaxLength(6)] // nvarchar(100)
        public string BadgeCode { get; set; }

        // Log dates
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } // To handle soft delete later

        // log user
        public int? CreatedBy { get; set; }
        
        public Client? Creator { get; set; }
        
        public List<Author>? Authors { get; set; }
        
        public List<Book>? Books { get; set; }
        
        public List<Borrow>? Borrows { get; set; }
        
        public List<Client>? Clients { get; set; }
        
        public List<Pubblisher>? Publishers { get; set; }
        
        public List<Tipology>? Tipologies { get; set; }

        public ICollection<Borrow> borrows { get; set; }
    }
}
