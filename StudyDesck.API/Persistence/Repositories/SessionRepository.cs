using Microsoft.EntityFrameworkCore;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Contexts;
using StudyDesck.API.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Persistence.Repositories
{
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
        }

        public async Task<Session> FindById(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        public async Task<Session> FindByTutorIdAndSessionId(int tutorId, int sessionId)
        {
            return await _context.Sessions.FindAsync(tutorId, sessionId);
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListByCategoryIdAsync(int categoryId)
        {
            return await _context.Sessions
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Include(p => p.Platform)
                .Include(p => p.Topic)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                            .ThenInclude(p => p.university)
                .Include(p => p.Tutor)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListByPlatformIdAsync(int platformId)
        {
            return await _context.Sessions
                .Where(p => p.PlatformId == platformId)
                .Include(p => p.Platform)
                .Include(p => p.Category)
                .Include(p => p.Topic)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                            .ThenInclude(p => p.university)
                .Include(p => p.Tutor)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListByTopicIdAsync(int topicId)
        {
            return await _context.Sessions
                .Where(p => p.TopicId == topicId)
                .Include(p => p.Topic)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                            .ThenInclude(p => p.university)
                .Include(p => p.Tutor)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                .Include(p => p.Platform)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListByTutorIdAsync(int tutorId)
        {
            return await _context.Sessions
                .Where(p => p.TutorId == tutorId)
                .Include(p => p.Tutor)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                .Include(p => p.Topic)
                    .ThenInclude(p => p.Course)
                        .ThenInclude(p => p.Career)
                            .ThenInclude(p => p.university)
                .Include(p => p.Platform)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public void Remove(Session session)
        {
            _context.Sessions.Remove(session);
        }

        public void Update(Session session)
        {
            _context.Sessions.Update(session);
        }
    }
}
