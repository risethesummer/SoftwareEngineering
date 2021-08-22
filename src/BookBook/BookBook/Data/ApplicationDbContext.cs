using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Models;

namespace BookBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieStaff>()
                .HasKey(k => new
                {
                    k.MovieID,
                    k.PersonID
                });
            modelBuilder.Entity<OrderProduct>()
                .HasKey(o => new
                {
                    o.OrderID,
                    o.ProductID
                });
            modelBuilder.Entity<TheaterProducts>()
                .HasKey(t => new
                {
                    t.TheaterID,
                    t.ProductID
                });
            modelBuilder.Entity<Review>()
                .HasKey(r => new 
                {
                    r.MovieID,
                    r.UserID
                });
            modelBuilder.Entity<UserAccount>()
                .HasAlternateKey(c => c.Account)
                .HasName("AlternateKey_Account");
            modelBuilder.Entity<Product>()
                .HasAlternateKey(c => c.Name)
                .HasName("AlternateKey_Product");
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<ResetPasswordRequest> ResetPasswordRequests { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<MovieStaff> MovieStaff { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<TheaterProducts> TheaterProducts { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
