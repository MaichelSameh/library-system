using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace library_system.Models
{
    public class Borrow
    {
        [Key]  // Explicit primary key
        [Required]
        public int Id { get; set; }

        [ForeignKey("Client_Id")]
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Book_Id")]
        [Required]
        public int BookId { get; set; }
        public Book book { get; set; }

        public DateTime BorrowDate { get; set; }
        public int BorrowDays { get; set; }

    }
}
