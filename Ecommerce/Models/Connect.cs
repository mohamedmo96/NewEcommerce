using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Connect
    {
        [Key]
        public int CoId { get; set; }
        [Required]

        public String Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}
