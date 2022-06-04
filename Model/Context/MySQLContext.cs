using Microsoft.EntityFrameworkCore;

namespace Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                Name = "Thainá",
                LastName = "Costa",
                Age = 25,
                Gender = "Female",
                Email = "thaina.biudes@gmail.com"
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 2,
                Name = "Thais",
                LastName = "Costa",
                Age = 34,
                Gender = "Female",
                Email = "ina_biudes@hotmail.com"
            });
        }
    }
}
