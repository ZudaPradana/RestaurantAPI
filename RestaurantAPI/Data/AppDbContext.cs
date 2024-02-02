using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Food> Foods { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionItems> TransactionItems { get; set; }


        //Relation Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Customer)
                .WithMany()  // Use WithMany() if you don't have a navigation property in the Customer model
                .HasForeignKey(t => t.CustomerId);

            modelBuilder.Entity<TransactionItems>()
                .HasOne(ti => ti.Food)
                .WithMany()
                .HasForeignKey(ti => ti.FoodId);

            modelBuilder.Entity<TransactionItems>()
                .HasOne(ti => ti.Transaction)
                .WithMany(t => t.Items)
                .HasForeignKey(ti => ti.TransactionId);
        }

    }
}
