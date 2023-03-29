using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Shoppingcart
    {
        [Key]
        public int CartId { get; set; }
        public int ProId { get; set; }
        [ForeignKey("ProId")]
        public virtual Product Product { get; set; }
        [Range(1,int.MaxValue , ErrorMessage ="Must Be More Than 1")]
        public int Qty { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
