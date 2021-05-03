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
        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Platform> Platforms { get; set; }

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
            //Relationships
            builder.Entity<Institute>()
                .HasMany(p => p.Careers)
                .WithOne(p => p.Institute)
                .HasForeignKey(p => p.InstituteId);

            //Career
            builder.Entity<Career>().ToTable("Careers");
            builder.Entity<Career>().HasKey(p => p.Id);
            builder.Entity<Career>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Career>().Property(p => p.Name).IsRequired().HasMaxLength(40);
            builder.Entity<Career>()
                .HasMany(p => p.Students)
                .WithOne(p => p.Career)
                .HasForeignKey(p => p.CareerId);
            builder.Entity<Career>()
                .HasMany(p => p.Courses)
                .WithOne(p => p.Career)
                .HasForeignKey(p => p.CareerId);

            //Student
            builder.Entity<Student>().ToTable("Students");
            builder.Entity<Student>().HasKey(p => p.Id);
            builder.Entity<Student>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Student>().Property(p => p.Name).IsRequired().HasMaxLength(40);
            builder.Entity<Student>().Property(p => p.LastName).IsRequired().HasMaxLength(40);
            builder.Entity<Student>().Property(p => p.Logo).IsRequired();
            builder.Entity<Student>().Property(p => p.Email).IsRequired().HasMaxLength(40);
            builder.Entity<Student>().Property(p => p.Password).IsRequired().HasMaxLength(40);


            //Category
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(15);
            builder.Entity<Category>().
                HasMany(p => p.Sessions).
                WithOne(p => p.Category).
                HasForeignKey(p => p.CategoryID);

            //Platform
            builder.Entity<Platform>().ToTable("Platforms");
            builder.Entity<Platform>().HasKey(p => p.Id);
            builder.Entity<Platform>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Platform>().Property(p => p.Name).IsRequired().HasMaxLength(15);
            builder.Entity<Platform>().Property(p => p.UrlReunion).IsRequired();

            //Session
            builder.Entity<Session>().ToTable("Sessions");
            builder.Entity<Session>().HasKey(p => p.Id);
            builder.Entity<Session>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Session>().Property(p => p.Title).IsRequired().HasMaxLength(15);
            builder.Entity<Session>().Property(p => p.Logo).IsRequired().HasMaxLength(30);
            builder.Entity<Session>().Property(p => p.Description).IsRequired();
            builder.Entity<Session>().Property(p => p.StartDate).IsRequired();
            builder.Entity<Session>().Property(p => p.EndDate).IsRequired();

            builder.Entity<Session>().Property(p => p.Price).IsRequired();
            builder.Entity<Session>().Property(p => p.QuantityMembers).IsRequired();
            //Relacion de 1 a 1 con tutor
            /*builder.Entity<Session>()
                .HasOne(a=>a.Tutor)
                .WithOne(b=>b.Session)
                .HasForeignKey<Tutor>(b => b.SessionId);*/

            //Relacion de 1 a muchos con Topics
            /*builder.Entity<Session>().
                HasMany(p => p.Topics).
                WithOne(p => p.Session).
                HasForeignKey(p => p.SessionId);*/

            //Course
            builder.Entity<Course>().ToTable("Courses");
            builder.Entity<Course>().HasKey(p => p.Id);
            builder.Entity<Course>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Course>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            //Relationships
            builder.Entity<Course>()
                .HasMany(p => p.Topics)
                .WithOne(p => p.Course)
                .HasForeignKey(p => p.CourseId);



            // end region
            builder.ApplySnakeCaseNamingConvetion();

        }
    }
}
