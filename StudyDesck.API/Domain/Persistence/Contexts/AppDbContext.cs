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
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<ExpertTopic> ExpertTopics { get; set; }
        public DbSet<Shedule> Shedules { get; set; }
<<<<<<< HEAD
        public DbSet<StudyMaterial> StudyMaterials { get; set; }
=======
        public DbSet<SessionReservation> SessionReservations { get; set; }
>>>>>>> feature/create_session-reservation

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
            //Relacion de 1 a muchos con Topics
            builder.Entity<Session>().
                HasMany(p => p.Topics).
                WithOne(p => p.Session).
                HasForeignKey(p => p.SessionId);

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

            // Shedule Entity
            builder.Entity<Shedule>().ToTable("Shedules");
            // Constraints
            builder.Entity<Shedule>().HasKey(p => p.Id);
            builder.Entity<Shedule>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Shedule>().Property(p => p.StarDate).IsRequired().HasMaxLength(30);
            builder.Entity<Shedule>().Property(p => p.EndDate).IsRequired().HasMaxLength(30);
            builder.Entity<Shedule>().Property(p => p.Date).IsRequired().HasMaxLength(30);
            // Relationships 

            // Tutor Entity
            builder.Entity<Tutor>().ToTable("Tutors");
            // Constraints
            builder.Entity<Tutor>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Tutor>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Tutor>().Property(p => p.LastName).IsRequired().HasMaxLength(30);
            builder.Entity<Tutor>().Property(p => p.Email).IsRequired().HasMaxLength(30);
            builder.Entity<Tutor>().Property(p => p.Password).IsRequired().HasMaxLength(30);
            builder.Entity<Tutor>().Property(p => p.InstituteName).IsRequired().HasMaxLength(30);
            // Relationships
            builder.Entity<Tutor>()
                .HasMany(p => p.Shedules)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.TutorId);
            builder.Entity<Tutor>()
                .HasMany(p => p.Sessions)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.TutorID);

            // ExpertTopic Entity
            builder.Entity<ExpertTopic>().ToTable("ExpertTopics");
            builder.Entity<ExpertTopic>().HasKey(p => new { p.TutorId, p.TopicId });
            // Relationships
            builder.Entity<ExpertTopic>()
                .HasOne(et => et.Tutor)
                .WithMany(t => t.ExpertTopics)
                .HasForeignKey(et => et.TutorId);
            builder.Entity<ExpertTopic>()
                .HasOne(et => et.Topic)
                .WithMany(to => to.ExpertTopics)
                .HasForeignKey(et => et.TopicId);

<<<<<<< HEAD
            //StudyMaterial Entity
            builder.Entity<StudyMaterial>().ToTable("StudyMaterials");
            // Constraints
            builder.Entity<StudyMaterial>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<StudyMaterial>().Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.Entity<StudyMaterial>().Property(p => p.Description).IsRequired().HasMaxLength(50);
            // Relationships
            //builder.Entity<StudyMaterial>()
            //    .HasMany(sym => sym.StudentMaterials)
            //    .WithOne(stm => stm.StudyMaterial)
            //    .HasForeignKey(stm => stm.StudyMaterialId);
            //builder.Entity<StudyMaterial>()
            //    .HasMany(sym => sym.SesionMaterials)
            //    .WithOne(sem => sem.StudyMaterial)
            //    .HasForeignKey(sem => sem.StudyMaterialId);
            //builder.Entity<StudyMaterial>()
            //    .HasOne(sm => sm.Topic)
            //    .WithMany(to => to.StudyMaterials)
            //    .HasForeignKey(sm => sm.TopicId);
=======

            //SessionReservation
            builder.Entity<SessionReservation>().ToTable("SessionReservations");
            builder.Entity<SessionReservation>().Property(sr => sr.Qualification);
            builder.Entity<SessionReservation>().Property(sr => sr.Confirmed);
            builder.Entity<SessionReservation>().HasKey(sr => new { sr.StudentId, sr.SessionId });
            builder.Entity<SessionReservation>()
                .HasOne(sr => sr.Session)
                .WithMany(s => s.SessionReservations)
                .HasForeignKey(sr => sr.SessionId);

            builder.Entity<SessionReservation>()
                .HasOne(sr => sr.Student)
                .WithMany(s => s.SessionReservations)
                .HasForeignKey(sr => sr.StudentId);
            

>>>>>>> feature/create_session-reservation
            // end region
            builder.ApplySnakeCaseNamingConvetion();

        }
    }
}
