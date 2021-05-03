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
        public readonly IUnitOfWork _unitOfWork;

        public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionResponse> DeleteAsync(int id)
        {
            var existingSession = await _sessionRepository.FindById(id);
            if (existingSession == null)
                return new SessionResponse("Session not found");
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

        public async Task<SessionResponse> SaveAsync(Session session)
        {
            try
            {
                await _sessionRepository.AddAsync(session);
                await _unitOfWork.CompleteAsync();
                return new SessionResponse(session);
            }
            catch (Exception e)
            {
                return new SessionResponse("Has ocurred an error saving the session " + e.Message);
            }
        }

        public async Task<SessionResponse> UpdateAsync(int id, Session session)
        {
            var existingSession = await _sessionRepository.FindById(id);
            if (existingSession == null)
                return new SessionResponse("session no encontrada");

            existingSession.Title = session.Title;
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
    }
}
