using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModel
{
    public class LoginVm
    {
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
