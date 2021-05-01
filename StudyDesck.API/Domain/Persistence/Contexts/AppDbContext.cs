//using StudyDesck.API.Domain.Models;
using StudyDesck.API.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<ProductTag> ProductTags { get; set; } // persistencia de una relacion muchos a muhcos
        //public DbSet<Tag> Tags { get; set; }

        // contructor for options:
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // overrides
        protected override void OnModelCreating(ModelBuilder builder)

        {
            base.OnModelCreating(builder);
            // my code:
 
            // end region
            builder.ApplySnakeCaseNamingConvetion();

        }
    }
}
