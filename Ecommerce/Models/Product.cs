using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Product
    {
        [Key]
        public int ProId { get; set; }
        [Required(ErrorMessage ="Required")]
        public String? ProName { get; set; }
        [Required(ErrorMessage = "Descreption Required")]
        public String? Descreption { get; set; }
        public decimal Price { get; set; }
        public string? ProImage { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public int CatId { get; set; }
        [ForeignKey("CatId")]
        public virtual Category ?Category { get; set; }

    }
}
