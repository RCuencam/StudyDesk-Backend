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
    public class TutorRepository : BaseRepository, ITutorRepository
    {
        public TutorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Tutor tutor)
        {
            await _context.Tutors.AddAsync(tutor);
        }

        public async Task<Tutor> FindById(int id)
        {
            return await _context.Tutors.FindAsync(id);
        }

        public async Task<IEnumerable<Tutor>> ListAsync()
        {
            return await _context.Tutors.ToListAsync();
        }

        public void Remove(Tutor tutor)
        {
            _context.Tutors.Remove(tutor);
        }

        public void Update(Tutor tutor)
        {
            _context.Tutors.Update(tutor);
        }

        public async Task<IEnumerable<Tutor>> ListByCourseIdAsync(int courseId)
        {
            return await _context.Tutors
                .Where(t => t.CourseId == courseId)
                .Include(t => t.Course)
                .ToListAsync();
        }
    }
}
