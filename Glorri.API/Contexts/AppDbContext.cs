using Glorri.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Glorri.API.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
