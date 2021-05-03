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
    public class SheduleRepository: BaseRepository, ISheduleRepository
    {
        public SheduleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Shedule shedule)
        {
            await _context.Shedules.AddAsync(shedule);
        }

        public async Task<Shedule> FindById(int id)
        {
            return await _context.Shedules.FindAsync(id);
        }

        public async Task<IEnumerable<Shedule>> ListAsync()
        {
            return await _context.Shedules.ToListAsync();
        }

        //public async Task<IEnumerable<Shedule>> ListByTutorIdAsync(int tutorId)
        //{
        //    return await _context.Shedules.
        //}

        public void Remove(Shedule shedule)
        {
            _context.Shedules.Remove(shedule);
        }

        public void Update(Shedule shedule)
        {
            _context.Shedules.Update(shedule);
        }
    }
}
