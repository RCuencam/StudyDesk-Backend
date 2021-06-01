using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ITutorReservationService
    {
        Task<IEnumerable<TutorReservation>> ListAsync();
        Task<TutorReservationResponse> GetByStudentIdAndTutorIdAndPlatformId(int studentId, int tutorId, int platformId);
        Task<TutorReservationResponse> GetByStudentId(int studentId);
        Task<TutorReservationResponse> GetByTutorId(int tutorId);
        Task<TutorReservationResponse> GetByPlatformId(int platformId);
        Task<TutorReservationResponse> AssignTutorReservationAsync(int studentId, int tutorId, int platformId, TutorReservation tutorReservation);
        Task<TutorReservationResponse> UnassignTutorReservationAsync(int studentId, int tutorId, int platformId);
        Task<TutorReservationResponse> UpdateTutorReservationAsync(int studentId, int tutorId, int platformId, TutorReservation tutorReservation);
    }
}
