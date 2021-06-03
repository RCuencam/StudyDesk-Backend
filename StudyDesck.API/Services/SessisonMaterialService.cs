using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyDesck.API.Services
{
    public class SessisonMaterialService : ISessionMaterialService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IStudyMaterialRepository _studyMaterialRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly ISessionMaterialRepository _sessionMaterialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SessisonMaterialService(ISessionRepository sessionRepository,
            IStudyMaterialRepository studyMaterialRepository,
            ITutorRepository tutorRepository,
            ISessionMaterialRepository sessionMaterialRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _studyMaterialRepository = studyMaterialRepository;
            _tutorRepository = tutorRepository;
            _sessionMaterialRepository = sessionMaterialRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionMaterialResponse> AssignSessionMaterialAsync(int sessionId, int materialId)
        {
            try
            {
                await _sessionMaterialRepository.AssignSessionMaterial(sessionId, materialId);
                await _unitOfWork.CompleteAsync();
                SessionMaterial sessionMaterial =
                    await _sessionMaterialRepository.FindBySessionIdAndStudyMaterial(sessionId, materialId);
                return new SessionMaterialResponse(sessionMaterial);
            }
            catch (Exception ex)
            {
                return new SessionMaterialResponse
                    ($"An error ocurred while assigning SessionMaterial to Session: {ex.Message}");
            }
        }

        public async Task<SessionMaterialResponse> AssignSessionMaterialAsync(int sessionId, SessionMaterial sessionMaterial)
        {
            try
            {
                var material = await _studyMaterialRepository.SaveAsync(sessionMaterial.StudyMaterial);
                await _unitOfWork.CompleteAsync();
                await _sessionMaterialRepository.AssignSessionMaterial(sessionId,
                    material.Id,
                    sessionMaterial.TutorId);
                await _unitOfWork.CompleteAsync();
                SessionMaterial result =
                    await _sessionMaterialRepository.FindBySessionIdAndStudyMaterial(sessionId, material.Id);
                return new SessionMaterialResponse(result);
            }
            catch (Exception ex)
            {
                return new SessionMaterialResponse
                    ($"An error ocurred while assigning SessionMaterial to Session: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SessionMaterial>> ListBySessionIdAsync(int sessionId)
        {
            return await _sessionMaterialRepository.ListBySessionIdAsync(sessionId);
        }

        public async Task<SessionMaterialResponse> UnassignSessionMaterialAsync(int sessionId, int materialId)
        {
            try
            {
                SessionMaterial sessionMaterial =
                    await _sessionMaterialRepository.FindBySessionIdAndStudyMaterial(sessionId, materialId);
                _sessionMaterialRepository.Remove(sessionMaterial);
                await _unitOfWork.CompleteAsync();

                var material = await _studyMaterialRepository.FindById(materialId);
                _studyMaterialRepository.Remove(material);
                await _unitOfWork.CompleteAsync();

                return new SessionMaterialResponse(sessionMaterial);
            }
            catch (Exception ex)
            {
                return new SessionMaterialResponse
                    ($"An error ocurred while assigning SessionMaterial from Session: {ex.Message}");
            }
        }


        //public async Task<SessionMaterialResponse> DeleteAsync(int sessionId, int studyMaterialId, int tutorId, int id)
        //{
        //    var existingSession = await _sessionRepository.FindById(sessionId);
        //    var existingStudyMaterial = await _studyMaterialRepository.FindById(studyMaterialId);
        //    var existingTutor = await _tutorRepository.FindById(tutorId);
        //    if (existingSession == null)
        //        return new SessionMaterialResponse("Session not found");
        //    if(existingStudyMaterial == null)
        //        return new SessionMaterialResponse("Study Material not found");
        //    if(existingTutor == null)
        //        return new SessionMaterialResponse("Tutor not found");
        //    var existingSessionMaterial = await _sessionMaterialRepository.FindById(id);

        //    if (existingSessionMaterial == null)
        //        return new SessionMaterialResponse("Session Material not found");
        //    try
        //    {
        //        _sessionMaterialRepository.Remove(existingSessionMaterial);
        //        await _unitOfWork.CompleteAsync();

        //        return new SessionMaterialResponse(existingSessionMaterial);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new SessionMaterialResponse($"An error ocurred while deleting the session material: {ex.Message}");
        //    }
        //}

        //public async Task<SessionMaterialResponse> GetByIdAndSessionId(int sessionId, int id)
        //{
        //    var existingSession = await _sessionRepository.FindById(sessionId);
        //    if (existingSession == null)
        //        return new SessionMaterialResponse("Session not found");
        //    var existingSessionMaterial = await _sessionMaterialRepository.FindById(id);
        //    if (existingSessionMaterial == null)
        //        return new SessionMaterialResponse("Session Material not found");
        //    return new SessionMaterialResponse(existingSessionMaterial);
        //}

        //public async Task<SessionMaterialResponse> GetByIdAndStudyMaterialId(int studyMaterialId, int id)
        //{
        //    var existingStudyMaterial = await _studyMaterialRepository.FindById(studyMaterialId);
        //    if (existingStudyMaterial == null)
        //        return new SessionMaterialResponse("Study Material not found");
        //    var existingSessionMaterial = await _sessionMaterialRepository.FindById(id);
        //    if (existingSessionMaterial == null)
        //        return new SessionMaterialResponse("Session Material not found");
        //    return new SessionMaterialResponse(existingSessionMaterial);
        //}

        //public async Task<SessionMaterialResponse> GetByIdAndTutorId(int tutorId, int id)
        //{
        //    var existingTutor = await _tutorRepository.FindById(tutorId);
        //    if (existingTutor == null)
        //        return new SessionMaterialResponse("Tutor not found");
        //    var existingSessionMaterial = await _sessionMaterialRepository.FindById(id);
        //    if (existingSessionMaterial == null)
        //        return new SessionMaterialResponse("Session Material not found");
        //    return new SessionMaterialResponse(existingSessionMaterial);
        //}

        //public async Task<IEnumerable<SessionMaterial>> ListBySessionIdAsync(int sessionId)
        //{
        //    return await _sessionMaterialRepository.ListBySessionIdAsync(sessionId);
        //}

        //public async Task<IEnumerable<SessionMaterial>> ListByStudyMaterialIdAsync(int studyMaterialId)
        //{
        //    return await _sessionMaterialRepository.ListByStudyMaterialIdAsync(studyMaterialId);
        //}

        //public async Task<IEnumerable<SessionMaterial>> ListByTutorIdAsync(int tutorId)
        //{
        //    return await _sessionMaterialRepository.ListByTutorIdAsync(tutorId);
        //}

        //public async Task<SessionMaterialResponse> SaveAsync(int sessionId, int studyMaterialId, int tutorId, SessionMaterial sessionMaterial)
        //{
        //    var existingSession = await _sessionRepository.FindById(sessionId);
        //    var existingStudyMaterial = await _studyMaterialRepository.FindById(studyMaterialId);
        //    var existingTutor = await _tutorRepository.FindById(tutorId);
        //    if (existingSession == null)
        //        return new SessionMaterialResponse("Session not found");
        //    if (existingStudyMaterial == null)
        //        return new SessionMaterialResponse("Study Material not found");
        //    if (existingTutor == null)
        //        return new SessionMaterialResponse("Tutor not found");
        //    sessionMaterial.SessionId = sessionId;
        //    sessionMaterial.Session = existingSession;
        //    sessionMaterial.StudyMaterialId = studyMaterialId;
        //    sessionMaterial.StudyMaterial = existingStudyMaterial;
        //    sessionMaterial.TutorId = tutorId;
        //    sessionMaterial.Tutor = existingTutor;
        //    try
        //    {
        //        await _sessionMaterialRepository.AddAsync(sessionMaterial);
        //        await _unitOfWork.CompleteAsync();

        //        return new SessionMaterialResponse(sessionMaterial);
        //    }
        //    catch(Exception ex)
        //    {
        //        return new SessionMaterialResponse($"An error ocurred while saving the Session material: {ex.Message}");
        //    }

        //}
    }
}