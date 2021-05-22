using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Services
{
    public class SessionReservationService : ISessionReservationService
    {
        private readonly ISessionReservationRepository _sessionResRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        
        public SessionReservationService(ISessionReservationRepository sessionResRepository, ISessionRepository sessionRepository, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _sessionResRepository = sessionResRepository;
            _sessionRepository = sessionRepository;
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionReservationResponse> FindByStudentIdAndSessionId(int studentId, int sessionId)
        {
            var existingSessionReserv = await _sessionResRepository.FindByStudentIdAndSessionId(studentId, sessionId);
            if (existingSessionReserv == null)
                return new SessionReservationResponse("This session reservation is not found");
            return new SessionReservationResponse(existingSessionReserv);
        }

        public async Task<IEnumerable<SessionReservation>> ListAsync()
        {
            return await _sessionResRepository.ListAsync();
        }

        public async Task<SessionReservationResponse> AssignSessionReservationAsync(int studentId, int sessionId, SessionReservation sessionReservation)
        {
            var student = await _studentRepository.FindById(studentId);
            var session = await _sessionRepository.FindById(sessionId);

            if(student == null || session == null)
                return new SessionReservationResponse("SessionId or StudentId not found");

            var existsIt = await _sessionResRepository.FindByStudentIdAndSessionId(studentId, sessionId);
            if (existsIt != null)
                return new SessionReservationResponse("This session reservation already exist");

            try
            {
                sessionReservation.Session = session;
                sessionReservation.Student = student;
                sessionReservation.SessionId = sessionId;
                sessionReservation.StudentId = studentId;

                await _sessionResRepository.AddAsync(sessionReservation);
                await _unitOfWork.CompleteAsync();
                return new SessionReservationResponse(sessionReservation);
            }
            catch (Exception e)
            {
                return new SessionReservationResponse($"Ocurrió un error: {e.Message}");
            }

        }

        public async Task<SessionReservationResponse> UpdateSessionReservationAsync(int studentId, int sessionId, SessionReservation sessionReservation)
        {
            var existingSessionReserv = await _sessionResRepository.FindByStudentIdAndSessionId(studentId, sessionId);
            if (existingSessionReserv == null)
                return new SessionReservationResponse("SessionReservation not found");

            try
            {
                existingSessionReserv.Confirmed = sessionReservation.Confirmed;
                existingSessionReserv.Qualification = sessionReservation.Qualification;

                _sessionResRepository.Update(existingSessionReserv);
                await _unitOfWork.CompleteAsync();
                return new SessionReservationResponse(existingSessionReserv);
            }
            catch (Exception e)
            {
                return new SessionReservationResponse($"Ocurrió un error: {e.Message}");
            }
        }

        public async Task<SessionReservationResponse> UnassignSessionReservationAsync(int studentId,int sessionId)
        {
            var existingSessionReserv = await _sessionResRepository.FindByStudentIdAndSessionId(studentId, sessionId);
            if (existingSessionReserv == null)
                return new SessionReservationResponse("SessionReservation not found");

            try
            {
                _sessionResRepository.Remove(existingSessionReserv);
                await _unitOfWork.CompleteAsync();
                return new SessionReservationResponse(existingSessionReserv);
            }
            catch (Exception e)
            {
                return new SessionReservationResponse($"Ocurrió un error: {e.Message}");
            }
        }

        


    }

}
