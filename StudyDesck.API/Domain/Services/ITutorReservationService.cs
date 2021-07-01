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
        Task<IEnumerable<TutorReservation>> ListByStudentIdAsync(int studentId);
        Task<IEnumerable<TutorReservation>> ListByTutorIdAsync(int tutorId);
        Task<IEnumerable<TutorReservation>> ListTutorReservationByTutorIdAsync(int tutorId);
        Task<TutorReservationResponse> SaveTutorReservation(int studentId, int tutorId, TutorReservation tutorReservation);
        Task<TutorReservationResponse> UpdateTutorReservation(int id, int studentId, int tutorId, TutorReservation tutorReservation);
        Task<TutorReservationResponse> DeleteTutorRerservationAsync(int id, int studentId, int tutorId);
    }
}
