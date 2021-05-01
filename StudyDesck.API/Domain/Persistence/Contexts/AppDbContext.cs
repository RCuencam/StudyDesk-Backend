//using StudyDesck.API.Domain.Models;
using StudyDesck.API.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudyDesck.API.Domain.Models;

namespace StudyDesck.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Career> Careers { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Student> Students { get; set; }

        // contructor for options:
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // overrides
        protected override void OnModelCreating(ModelBuilder builder)

        {
            base.OnModelCreating(builder);
            // my code:
            //Institute

            builder.Entity<Institute>().ToTable("Institutes");
            builder.Entity<Institute>().HasKey(p => p.Id);
            builder.Entity<Institute>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Institute>().Property(p => p.Name).IsRequired().HasMaxLength(40);
            //Relationships Institute
            builder.Entity<Institute>()
                .HasMany(p => p.Careers)
                .WithOne(p => p.Institute)
                .HasForeignKey(p => p.InstituteId);


            //Career
            builder.Entity<Career>().ToTable("Careers");
            builder.Entity<Career>().HasKey(p => p.Id);
            builder.Entity<Career>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Career>().Property(p => p.Name).IsRequired().HasMaxLength(40);
            //Relationships Career
                
                


            // end region
            builder.ApplySnakeCaseNamingConvetion();

        }
    }
}
