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
    public class InstituteRepository : BaseRepository, IInstituteRepository
    {
        public InstituteRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Institute institute)
        {
            await _context.Institutes.AddAsync(institute);
        }

        public async Task<Institute> FindById(int id)
        {
            return await _context.Institutes.FindAsync(id);
        }

        public async Task<IEnumerable<Institute>> ListAsync()
        {
            return await _context.Institutes.ToListAsync();
        }

        public void Remove(Institute institute)
        {
            _context.Institutes.Remove(institute);
        }

        public void Update(Institute institute)
        {
            _context.Institutes.Update(institute);
        }
    }
}
