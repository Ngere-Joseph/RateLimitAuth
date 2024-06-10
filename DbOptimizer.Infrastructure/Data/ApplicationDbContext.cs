using DbOptimizer.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbOptimizer.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
    }
}
