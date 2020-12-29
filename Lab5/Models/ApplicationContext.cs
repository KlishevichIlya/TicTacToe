using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<User>().HasKey(x => x.Id);
    
        }
    }
}
