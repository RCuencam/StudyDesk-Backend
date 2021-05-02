using Microsoft.EntityFrameworkCore;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Contexts;
using StudyDesck.API.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Persistence.Repositories
{
    public class CourseRepository : BaseRepository, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);    
        }

        public async Task<Course> FindById(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<IEnumerable<Course>> ListAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public void Remove(Course course)
        {
            _context.Courses.Remove(course);
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
        }
    }
}
