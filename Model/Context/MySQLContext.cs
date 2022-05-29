using Custumers.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Custumers.API.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() {}
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Custumer> Custumer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Custumer>().HasData(new Custumer
            {
                Id = 1,
                Name = "Thainá",
                LastName = "Costa",
                Age = 25,
                Gender = "Female"
            });
            modelBuilder.Entity<Custumer>().HasData(new Custumer
            {
                Id = 2,
                Name = "Thais",
                LastName = "Costa",
                Age = 34,
                Gender = "Female"
            });
        }
    }
}
