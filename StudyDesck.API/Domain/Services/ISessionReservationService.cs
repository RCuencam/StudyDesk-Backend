using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ISessionReservationService
    {
        Task<IEnumerable<SessionReservation>> ListAsync();
        Task<SessionReservationResponse> GetByStudentIdAndSessionId(int studentId, int sessionId);
        Task<SessionReservationResponse> AssignSessionReservationAsync(int studentId, int sessionId, SessionReservation sessionReservation);
        Task<SessionReservationResponse> UnassignSessionReservationAsync(int studentId, int sessionId);
        Task<SessionReservationResponse> UpdateSessionReservationAsync(int studentId, int sessionId, SessionReservation sessionReservation);

    }
}
