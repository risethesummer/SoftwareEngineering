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
            modelBuilder.Entity<UserAccount>()
                .HasAlternateKey(c => c.Account)
                .HasName("AlternateKey_Account");
        }


        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<ResetPasswordRequest> ResetPasswordRequests { get; set; }

    }
}
