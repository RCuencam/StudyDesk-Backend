using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<IEnumerable<Session>> ListByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Session>> ListByPlatformIdAsync(int platformId);
        Task<IEnumerable<Session>> ListByTutorIdAsync(int tutorId);
        Task<IEnumerable<Session>> ListByTopicIdAsync(int topicId);

        Task AddAsync(Session session);
        Task<Session> FindById(int id);
        Task<Session> FindByTutorIdAndSessionId(int tutorId, int sessionId);
        void Update(Session session);
        void Remove(Session session);
    }
}

