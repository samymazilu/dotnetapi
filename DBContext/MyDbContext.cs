using Microsoft.EntityFrameworkCore;
using MyAPI.Entities;
public class MyDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().Navigation(x => x.Payments).AutoInclude();
         modelBuilder.Entity<Transaction>().Navigation(x => x.Customer).AutoInclude();
          modelBuilder.Entity<Transaction>().Navigation(x => x.Articles).AutoInclude();
    }
      
}
