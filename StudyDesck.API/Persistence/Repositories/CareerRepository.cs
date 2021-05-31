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

        public async Task<IEnumerable<Career>> FindByInstituteIdAndCareerId(int instituteId, int careerId)
        {
            return await _context.Careers
                .Where(c => c.InstituteId == instituteId && c.Id== careerId)
                .Include(c => c.Institute)
                .ToListAsync();
        }

        public async Task<IEnumerable<Career>> FindByInstituteIdAsync(int instituteId)
        {
            return await _context.Careers
                .Where(c => c.InstituteId == instituteId)
                .Include(c => c.Institute)
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
