using Microsoft.EntityFrameworkCore;
using Rentacar.Models;

namespace Rentacar.DbCon
{
    public class RentDbContext:DbContext
    {
        public RentDbContext(DbContextOptions<RentDbContext> options) : base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}