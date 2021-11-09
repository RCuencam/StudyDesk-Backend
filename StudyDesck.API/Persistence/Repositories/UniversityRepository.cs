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
    public class universityRepository : BaseRepository, IUniversityRepository
    {
        public universityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(University university)
        {
            await _context.Universities.AddAsync(university);
        }

        public async Task<University> FindById(int id)
        {
            return await _context.Universities.FindAsync(id);
        }

        public async Task<IEnumerable<University>> ListAsync()
        {
            return await _context.Universities.ToListAsync();
        }

        public void Remove(University university)
        {
            _context.Universities.Remove(university);
        }

        public void Update(University university)
        {
            _context.Universities.Update(university);
        }
    }
}
