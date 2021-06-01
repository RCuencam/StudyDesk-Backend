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
    public class ScheduleRepository: BaseRepository, IScheduleRepository
    {
        public ScheduleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Schedule shedule)
        {
            await _context.Schedules.AddAsync(shedule);
        }

        public async Task<Schedule> FindById( int sheduleId)
        {
            return await _context.Schedules.FindAsync(sheduleId);
        }
        public async Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId)
        {
            return await _context.Schedules
                .Where(s => s.TutorId == tutorId)
                .Include(s => s.Tutor)
                .ToListAsync();
        }
        public async Task<IEnumerable<Schedule>> ListAsync()
        {
            return await _context.Schedules.ToListAsync();
        }
        public void Remove(Schedule shedule)
        {
            _context.Schedules.Remove(shedule);
        }

        public void Update(Schedule shedule)
        {
            _context.Schedules.Update(shedule);
        }
    }
}
