using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<IEnumerable<Session>> ListByStudentIdAsync(int studentId);
        Task<IEnumerable<Session>> ListByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Session>> ListByPlatformIdAsync(int platformId);
        Task<IEnumerable<Session>> ListByTutorIdAsync(int tutorId);
        Task<IEnumerable<Session>> ListByTopicIdAsync(int topicId);
        Task<SessionResponse> GetByIdAsync(int id);
        Task<SessionResponse> SaveAsync(int tutorId, Session session);
        Task<SessionResponse> UpdateAsync(int tutorId,int id, Session session);
        Task<SessionResponse> DeleteAsync(int tutorId,int id);
    }
}
