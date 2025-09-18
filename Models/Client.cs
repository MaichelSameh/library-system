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

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        //public bool? isHidden { get; set; }

        public ICollection<Borrow> borrows { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
