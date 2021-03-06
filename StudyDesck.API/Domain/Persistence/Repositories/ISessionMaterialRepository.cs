using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ISessionMaterialRepository
    {
        Task<IEnumerable<SessionMaterial>> ListBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionMaterial>> ListByStudyMaterialIdAsync(int studyMaterialId);
        Task<IEnumerable<SessionMaterial>> ListByTutorIdAsync(int tutorId);
        Task<SessionMaterial> FindBySessionIdAndStudyMaterial(int sessionId, int studyMaterial);
        Task AddAsync(SessionMaterial sessionMaterial);
        void Remove(SessionMaterial sessionMaterial);

        Task AssignSessionMaterial(int sessionId, int materialId, int tutorId);
        Task AssignSessionMaterial(int sessionId, int materialId);
        Task UnassignStudyMaterial(int sessionId, int materialId);
    }
}
