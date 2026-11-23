using Microsoft.EntityFrameworkCore;
using PackApi.Entities;

namespace PackApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Gear> Gear { get; set; }
    }
}