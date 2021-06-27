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
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<StudyMaterial> StudyMaterials { get; set; }
        public DbSet<SessionReservation> SessionReservations { get; set; }
        public DbSet<StudentMaterial> StudentMaterials { get; set; }
        public DbSet<TutorReservation> TutorReservations { get; set; }
        public DbSet<SessionMaterial> SessionMaterials { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

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
            // Relationships
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
            builder.Entity<Student>().Property(p => p.Password).IsRequired();

            //Category
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(15);
            // Relationship
            builder.Entity<Category>().
                HasMany(p => p.Sessions).
                WithOne(p => p.Category).
                HasForeignKey(p => p.CategoryId);

            //Platform
            builder.Entity<Platform>().ToTable("Platforms");
            builder.Entity<Platform>().HasKey(p => p.Id);
            builder.Entity<Platform>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Platform>().Property(p => p.Name).IsRequired().HasMaxLength(15);
            builder.Entity<Platform>().Property(p => p.PlatformUrl).IsRequired();

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
            //Relationships
            builder.Entity<Session>()
                .HasOne(s => s.Platform)
                .WithMany(p => p.Sessions)
                .HasForeignKey(s => s.PlatformId);
            builder.Entity<Session>().
                HasOne(p => p.Topic).
                WithMany(t => t.Sessions).
                HasForeignKey(p => p.TopicId);

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

            // Schedule Entity
            builder.Entity<Schedule>().ToTable("Schedules");
            builder.Entity<Schedule>().HasKey(p => p.Id);
            builder.Entity<Schedule>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Schedule>().Property(p => p.StarDate).IsRequired();
            builder.Entity<Schedule>().Property(p => p.EndDate).IsRequired();

            // Tutor Entity
            builder.Entity<Tutor>().ToTable("Tutors");
            builder.Entity<Tutor>().HasKey(p => p.Id);
            builder.Entity<Tutor>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Tutor>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Tutor>().Property(p => p.LastName).IsRequired().HasMaxLength(30);
            builder.Entity<Tutor>().Property(p => p.Email).IsRequired().HasMaxLength(30);
            builder.Entity<Tutor>().Property(p => p.Password).IsRequired();
            // Relationships
            builder.Entity<Tutor>()
                .HasOne(t => t.Career)
                .WithMany(c => c.Tutors)
                .HasForeignKey(t => t.CareerId);
            builder.Entity<Tutor>()
                .HasMany(p => p.Shedules)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.TutorId);
            builder.Entity<Tutor>()
                .HasMany(p => p.Sessions)
                .WithOne(p => p.Tutor)
                .HasForeignKey(p => p.TutorId);

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

            //StudyMaterial Entity
            builder.Entity<StudyMaterial>().ToTable("StudyMaterials");
            builder.Entity<StudyMaterial>().HasKey(p => p.Id);
            builder.Entity<StudyMaterial>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<StudyMaterial>().Property(p => p.Title).IsRequired().HasMaxLength(30);
            builder.Entity<StudyMaterial>().Property(p => p.Description).IsRequired().HasMaxLength(50);
            // Relationships
            builder.Entity<StudyMaterial>()
                .HasOne(sm => sm.Topic)
                .WithMany(to => to.StudyMaterials)
                .HasForeignKey(sm => sm.TopicId);

            //SessionReservation
            builder.Entity<SessionReservation>().ToTable("SessionReservations");
            builder.Entity<SessionReservation>().HasKey(sr => new { sr.StudentId, sr.SessionId });
            builder.Entity<SessionReservation>().Property(sr => sr.Qualification);
            builder.Entity<SessionReservation>().Property(sr => sr.Confirmed);
            // relationships
            builder.Entity<SessionReservation>()
                .HasOne(sr => sr.Session)
                .WithMany(s => s.SessionReservations)
                .HasForeignKey(sr => sr.SessionId);
            builder.Entity<SessionReservation>()
                .HasOne(sr => sr.Student)
                .WithMany(s => s.SessionReservations)
                .HasForeignKey(sr => sr.StudentId);
            
            // StudentMaterial Entity
            builder.Entity<StudentMaterial>().ToTable("StudentMaterials");
            builder.Entity<StudentMaterial>().HasKey(sm => new { sm.StudentId, sm.StudyMaterialId });
            // relationships
            builder.Entity<StudentMaterial>()
                .HasOne(sms => sms.student)
                .WithMany(s => s.StudentMaterials)
                .HasForeignKey(sms => sms.StudentId);
            builder.Entity<StudentMaterial>()
                .HasOne(sms => sms.StudyMaterial)
                .WithMany(sm => sm.StudentMaterials)
                .HasForeignKey(sms => sms.StudyMaterialId);
            builder.Entity<StudentMaterial>()
                .HasOne(sms => sms.Category)
                .WithMany(c => c.StudentMaterials)
                .HasForeignKey(sms => sms.CategoryId);
            builder.Entity<StudentMaterial>()
                .HasOne(sms => sms.Institute)
                .WithMany(i => i.StudentMaterials)
                .HasForeignKey(sms => sms.InstituteId);

            builder.Entity<TutorReservation>().ToTable("TutorReservations");
            builder.Entity<TutorReservation>().HasKey(tr => new { tr.TutorId, tr.StudentId });
            builder.Entity<TutorReservation>().HasKey(tr => tr.Id);
            builder.Entity<TutorReservation>().Property(tr => tr.Id).ValueGeneratedOnAdd();
            //relationships
            builder.Entity<TutorReservation>()
                .HasOne(tr => tr.Tutor)
                .WithMany(tr => tr.TutorReservations)
                .HasForeignKey(tr => tr.TutorId);
            builder.Entity<TutorReservation>()
                .HasOne(tr => tr.Student)
                .WithMany(tr => tr.TutorReservations)
                .HasForeignKey(tr => tr.StudentId);

            //SessionMaterial Entity
            builder.Entity<SessionMaterial>().ToTable("SessionMaterials");
            builder.Entity<SessionMaterial>().HasKey(snm => new { snm.SessionId, snm.TutorId });
            builder.Entity<SessionMaterial>()
                .HasOne(snm => snm.Session)
                .WithMany(s => s.SessionMaterials)
                .HasForeignKey(snm => snm.SessionId);
            builder.Entity<SessionMaterial>()
                .HasOne(snm => snm.StudyMaterial)
                .WithMany(sym => sym.SessionMaterials)
                .HasForeignKey(snm => snm.StudyMaterialId);
            builder.Entity<SessionMaterial>()
                .HasOne(snm => snm.Tutor)
                .WithMany(t => t.SessionMaterials)
                .HasForeignKey(snm => snm.TutorId);


            // end region
            builder.ApplySnakeCaseNamingConvetion();
        }
    }
}
