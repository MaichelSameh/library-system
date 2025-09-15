using System.ComponentModel.DataAnnotations;

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
        public string Address { get; set; }
        public string FiscalCode { get; set; }

        [MaxLength(6)] // nvarchar(100)
        public string BadgeCode { get; set; }

        public ICollection<Borrow> borrows { get; set; }
    }
}
