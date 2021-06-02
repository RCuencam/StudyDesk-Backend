using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ISessionMaterialService
    {
        //Task<IEnumerable<SessionMaterial>> ListBySessionIdAsync(int sessionId);
        //Task<SessionMaterialResponse> GetByIdAndSessionId(int sessionId, int id);
        //Task<IEnumerable<SessionMaterial>> ListByStudyMaterialIdAsync(int studyMaterialId);
        //Task<SessionMaterialResponse> GetByIdAndStudyMaterialId(int studyMaterialId, int id);
        //Task<IEnumerable<SessionMaterial>> ListByTutorIdAsync(int tutorId);
        //Task<SessionMaterialResponse> GetByIdAndTutorId(int tutorId, int id);
        //Task<SessionMaterialResponse> SaveAsync(int sessionId, int studyMaterialId, int tutorId, SessionMaterial sessionMaterial);
        //Task<SessionMaterialResponse> DeleteAsync(int sessionId, int studyMaterialId, int tutorId, int id);


        Task<IEnumerable<SessionMaterial>> ListBySessionIdAsync(int sessionId);
        Task<SessionMaterialResponse> AssignSessionMaterialAsync(int sessionId, long materialId);
        Task<SessionMaterialResponse> UnassignSessionMaterialAsync(int sessionId, long materialId);
        Task<SessionMaterialResponse> AssignSessionMaterialAsync(int sessionId, SessionMaterial sessionMaterial);

    }
}
