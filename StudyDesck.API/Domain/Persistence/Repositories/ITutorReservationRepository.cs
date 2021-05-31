using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ITutorReservationRepository
    {
        Task<IEnumerable<TutorReservation>> ListAsync();
        Task<IEnumerable<TutorReservation>> ListByStudentIdAsync(int studentId);
        Task<IEnumerable<TutorReservation>> ListByTutorIdAsync(int tutorId);
        Task<IEnumerable<TutorReservation>> ListByPlatformIdAsync(int platformId);
        Task<TutorReservation> FindByStudentIdAndTutorIdAndPlatformId(int studentId, int tutorId, int platformId);
        Task AddAsync(TutorReservation TutorReservation);
        void Remove(TutorReservation TutorReservation);
        void Update(TutorReservation TutorReservation);
    }
}
