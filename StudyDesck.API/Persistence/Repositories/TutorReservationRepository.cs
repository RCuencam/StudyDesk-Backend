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
    public class TutorReservationRepository : BaseRepository, ITutorReservationRepository
    {
        public TutorReservationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(TutorReservation TutorReservation)
        {
            await _context.TutorReservations.AddAsync(TutorReservation);
        }

        public async Task<TutorReservation> FindById(int id, int studentId, int tutorId)
        {
            return await _context.TutorReservations.FindAsync(id);
        }

        public async Task<IEnumerable<TutorReservation>> ListAllByTutorIdAsync(int tutorId)
        {
            return await _context.TutorReservations
                 .Where(tr => tr.TutorId == tutorId)
                 .Include(tr => tr.Tutor)
                 .ToListAsync();
        }

        public async Task<IEnumerable<TutorReservation>> ListByStudentIdAsync(int studentId)
        {
            return await _context.TutorReservations
                .Where(tr => tr.StudentId == studentId)
                 .Include(tr => tr.Tutor)
                .ToListAsync();
        }

        public async Task<IEnumerable<TutorReservation>> ListByTutorIdAsync(int tutorId)
        {
            return await _context.TutorReservations
                .Where(tr => tr.TutorId == tutorId)
                .Include(tr => tr.Student)
                .ToListAsync();
        }

        public void Remove(TutorReservation tutorReservation)
        {
            _context.TutorReservations.Remove(tutorReservation);
        }

        public void Update(TutorReservation TutorReservation)
        {
            _context.TutorReservations.Update(TutorReservation);
        }
    }
}
