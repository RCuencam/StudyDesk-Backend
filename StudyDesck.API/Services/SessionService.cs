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
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly ISessionReservationRepository _sessionReservationRepository;
        public readonly IUnitOfWork _unitOfWork;

        public SessionService(ISessionRepository sessionRepository, ISessionReservationRepository sessionReservationRepository, ITutorRepository tutorRepository,IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _sessionReservationRepository = sessionReservationRepository;
            _tutorRepository = tutorRepository;
            _unitOfWork = unitOfWork;
        }

        

        public async Task<SessionResponse> GetByIdAsync(int id)
        {
            var existingSession = await _sessionRepository.FindById(id);
            if (existingSession == null)
                return new SessionResponse("Session not found");

            return new SessionResponse(existingSession);
        }

        

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _sessionRepository.ListAsync();
        }

        public async Task<IEnumerable<Session>> ListByCategoryIdAsync(int categoryId)
        {
            return await _sessionRepository.ListByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Session>> ListByPlatformIdAsync(int platformId)
        {
            return await _sessionRepository.ListByPlatformIdAsync(platformId);
        }

        public async Task<IEnumerable<Session>> ListByTutorIdAsync(int tutorId)
        {
            return await _sessionRepository.ListByTutorIdAsync(tutorId);
        }

        public async Task<IEnumerable<Session>> ListByTopicIdAsync(int topicId)
        {
            return await _sessionRepository.ListByTopicIdAsync(topicId);
        }

        public async Task<IEnumerable<Session>> ListByStudentIdAsync(int studentId)
        {
            var sessionReservations = await _sessionReservationRepository.ListByStudentIdAsync(studentId);
            var sessions = sessionReservations.Select(sr => sr.Session).ToList();
            return sessions;
        }

        public async Task<SessionResponse> SaveAsync(int tutorId,Session session)
        {
            var existingTutor = await _tutorRepository.FindById(tutorId);

            if (existingTutor == null)
                return new SessionResponse("Id de tutor no encontrado");

            try
            {
                session.TutorId = tutorId;
                await _sessionRepository.AddAsync(session);
                await _unitOfWork.CompleteAsync();
                return new SessionResponse(session);
            }
            catch (Exception e)
            {
                return new SessionResponse("Has ocurred an error saving the session " + e.Message);
            }
        }

        public async Task<SessionResponse> UpdateAsync(int tutorId, int id, Session session)
        {
            var existingSession = await _sessionRepository.FindByTutorIdAndSessionId(tutorId,id);
            if (existingSession == null)
                return new SessionResponse("Session no encontrada");

            //var existingSession = await _sessionRepository.FindById(id);
            //if (existingSession == null)
            //    return new SessionResponse("session no encontrada");

            existingSession.Title = session.Title;
            existingSession.Logo = session.Logo;
            existingSession.Description = session.Description;
            existingSession.StartDate = session.StartDate;
            existingSession.EndDate = session.EndDate;
            existingSession.QuantityMembers = session.QuantityMembers;
            existingSession.Price = session.Price;
            try
            {
                _sessionRepository.Update(existingSession);
                await _unitOfWork.CompleteAsync();
                return new SessionResponse(existingSession);
            }
            catch (Exception e)
            {
                return new SessionResponse("Has ocurred an error updating the session " + e.Message);
            }
        }

        public async Task<SessionResponse> DeleteAsync(int tutorId, int id)
        {
            var existingSession = await _sessionRepository.FindByTutorIdAndSessionId(tutorId, id);
            if (existingSession == null)
                return new SessionResponse("Session no encontrada");

            try
            {
                _sessionRepository.Remove(existingSession);
                return new SessionResponse(existingSession);
            }
            catch (Exception e)
            {
                return new SessionResponse("Has ocurred an error deleting the session " + e.Message);
            }
        }

        
    }
}
