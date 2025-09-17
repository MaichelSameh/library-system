using System.ComponentModel.DataAnnotations;

namespace library_system.Models
{
    public class Config
    {
        [Key]
        [Required]
        public int ID {  get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; } 

        [Required]
        [MaxLength(300)]
        public string Code {  get; set; }

        [Required]
        [MaxLength(300)]
        public string Value {  get; set; }

    }
}
