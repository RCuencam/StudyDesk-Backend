using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ITutorReservationRepository
    {
        Task<IEnumerable<TutorReservation>> ListByStudentIdAsync(int studentId);
        Task<IEnumerable<TutorReservation>> ListByTutorIdAsync(int tutorId);
        Task AddAsync(TutorReservation TutorReservation);
        void Remove(TutorReservation TutorReservation);
        void Update(TutorReservation TutorReservation);
        Task<IEnumerable<TutorReservation>> ListAllByTutorIdAsync(int tutorId);
        Task<TutorReservation> FindById(int id, int studentId, int tutorId);
    }
}
