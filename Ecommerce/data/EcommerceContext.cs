using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Ecommerce.data
{
    public class EcommerceContext :IdentityDbContext<ApplicationUser>
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Connect> Connects { get; set; }
        public DbSet<Shoppingcart> Shoppingcarts { get; set; }

    }
}

