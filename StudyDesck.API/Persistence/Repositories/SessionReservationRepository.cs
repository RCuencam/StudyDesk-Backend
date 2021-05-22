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
    public class SessionReservationRepository : BaseRepository, ISessionReservationRepository
    {
        public SessionReservationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SessionReservation SessionReservation)
        {
            await _context.SessionReservations.AddAsync(SessionReservation);
        }

        

        public async Task<SessionReservation> FindByStudentIdAndSessionId(int studentId, int sessionId)
        {
            return await _context.SessionReservations.FindAsync(studentId, sessionId);
        }

        public async Task<IEnumerable<SessionReservation>> ListAsync()
        {
            return await _context.SessionReservations
                .Include(sr => sr.Student)
                .Include(sr => sr.Session)
                .ToListAsync();
          
        }

        public async Task<IEnumerable<SessionReservation>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.SessionReservations
                .Where(sr => sr.SessionId == sessionId)
                .Include(sr => sr.Student)
                .Include(sr => sr.Session)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionReservation>> ListByStudentIdAsync(int studentId)
        {
            return await _context.SessionReservations
                .Where(sr => sr.StudentId == studentId)
                .Include(sr => sr.Student)
                .Include(sr => sr.Session)
                .ToListAsync();
        }

        public void Remove(SessionReservation SessionReservation)
        {
            _context.SessionReservations.Remove(SessionReservation);
        }

        public void Update(SessionReservation SessionReservation)
        {
            _context.SessionReservations.Update(SessionReservation);
        }
    }
}
