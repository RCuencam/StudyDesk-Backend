using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ISessionReservationRepository
    {
        Task<IEnumerable<SessionReservation>> ListAsync();
        Task<IEnumerable<SessionReservation>> ListByStudentIdAsync(int studentId);
        Task<IEnumerable<SessionReservation>> ListBySessionIdAsync(int sessionId);
        Task<SessionReservation> FindByStudentIdAndSessionId(int studentId, int sessionId);
        Task AddAsync(SessionReservation SessionReservation);
        void Remove(SessionReservation SessionReservation);
        void Update(SessionReservation SessionReservation);
    }
}
