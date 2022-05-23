using BankingSystem.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.API.DbContexts
{
    public class BankContext : DbContext
    {

        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.AccountNumber)
                .IsUnique(); 
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                .IsUnique();
            });
        }

    }
}
