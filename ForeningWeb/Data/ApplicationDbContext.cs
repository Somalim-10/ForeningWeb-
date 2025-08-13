using ForeningWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Om> Om { get; set; }
        public DbSet<Kontakt> Kontakter { get; set; }
        public DbSet<Donation> Donationer { get; set; }
    }
}
