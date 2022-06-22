using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kcal.Models;

namespace Kcal.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Kcal.Models.ProductCategory>? ProductCategory { get; set; }
        public DbSet<Kcal.Models.Product>? Product { get; set; }
        public DbSet<Kcal.Models.Consumption>? Consumption { get; set; }
    }
}