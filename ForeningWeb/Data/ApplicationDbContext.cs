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

        public DbSet<Event> Events => Set<Event>();
    }
}
