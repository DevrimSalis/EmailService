using EmailService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Infrastructure.Persistence
{
    public class EmailContext : DbContext
    {
        public EmailContext(DbContextOptions<EmailContext> options) : base(options)
        {
        }

        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .Property(e => e.To)
                .IsRequired();

            modelBuilder.Entity<Email>()
                .Property(e => e.Subject)
                .IsRequired();

            modelBuilder.Entity<Email>()
                .Property(e => e.Body)
                .IsRequired();
        }
    }
}

