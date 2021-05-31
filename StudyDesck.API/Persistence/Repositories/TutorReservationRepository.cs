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

        public async Task<TutorReservation> FindByStudentIdAndTutorIdAndPlatformId(int studentId, int tutorId, int platformId)
        {
            return await _context.TutorReservations.FindAsync(studentId, tutorId, platformId);
        }

        public async Task<TutorReservation> FindByStudentId(int studentId)
        {
            return await _context.TutorReservations.FindAsync(studentId);
        }

        public async Task<TutorReservation> FindByTutorId(int tutorId)
        {
            return await _context.TutorReservations.FindAsync(tutorId);
        }

        public async Task<TutorReservation> FindByPlatformId(int platformId)
        {
            return await _context.TutorReservations.FindAsync(platformId);
        }

        public async Task<IEnumerable<TutorReservation>> ListAsync()
        {
            return await _context.TutorReservations
                .Include(tr => tr.Student)
                .Include(tr => tr.Tutor)
                .Include(tr => tr.Platform)
                .ToListAsync();
        }

        public async Task<IEnumerable<TutorReservation>> ListByPlatformIdAsync(int platformId)
        {
            return await _context.TutorReservations
                .Where(tr => tr.PlatformId== platformId)
                .Include(tr => tr.Student)
                .Include(tr => tr.Tutor)
                .ToListAsync();
        }

        public async Task<IEnumerable<TutorReservation>> ListByStudentIdAsync(int studentId)
        {
            return await _context.TutorReservations
                .Where(tr => tr.StudentId == studentId)
                .Include(tr => tr.Tutor)
                .Include(tr => tr.Platform)
                .ToListAsync();
        }

        public async Task<IEnumerable<TutorReservation>> ListByTutorIdAsync(int tutorId)
        {
            return await _context.TutorReservations
                .Where(tr => tr.TutorId == tutorId)
                .Include(tr => tr.Student)
                .Include(tr => tr.Platform)
                .ToListAsync();
        }

        public void Remove(TutorReservation TutorReservation)
        {
            _context.TutorReservations.Remove(TutorReservation);
        }

        public void Update(TutorReservation TutorReservation)
        {
            _context.TutorReservations.Update(TutorReservation);
        }
    }
}
