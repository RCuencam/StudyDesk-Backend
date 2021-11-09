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
    public class CareerRepository : BaseRepository, ICareerRepository
    {
        public CareerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Career career)
        {
            await _context.Careers.AddAsync(career);
        }

        public async Task<Career> FindById(int id)
        {
            return await _context.Careers.FindAsync(id);
        }

        public async Task<IEnumerable<Career>> FindByuniversityIdAndCareerId(int universityId, int careerId)
        {
            return await _context.Careers
                .Where(c => c.universityId == universityId && c.Id== careerId)
                .Include(c => c.university)
                .ToListAsync();
        }

        public async Task<IEnumerable<Career>> FindByuniversityIdAsync(int universityId)
        {
            return await _context.Careers
                .Where(c => c.universityId == universityId)
                .Include(c => c.university)
                .ToListAsync();
        }

        public async Task<IEnumerable<Career>> ListAsync()
        {
            return await _context.Careers.ToListAsync();
        }

        public void Remove(Career career)
        {
            _context.Careers.Remove(career);
        }

        public void Update(Career career)
        {
            _context.Careers.Update(career);
        }
    }
}
