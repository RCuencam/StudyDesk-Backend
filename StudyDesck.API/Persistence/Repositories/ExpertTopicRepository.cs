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
    public class ExpertTopicRepository : BaseRepository, IExpertTopicRepository
    {
        public ExpertTopicRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ExpertTopic expertTopic)
        {
            await _context.ExpertTopics.AddAsync(expertTopic);
        }

        public async Task AssignExpertTopic(int tutorId, int topicId)
        {
            ExpertTopic expertTopic = await FindByTutorIdAndTopicId(tutorId, topicId);
            if (expertTopic == null)
            {
                expertTopic = new ExpertTopic { TutorId = tutorId, TopicId = topicId };
                await AddAsync(expertTopic);
            }
        }

        public async Task<ExpertTopic> FindByTutorIdAndTopicId(int tutorId, int topicId)
        {
            return await _context.ExpertTopics.FindAsync(tutorId, topicId);
        }

        public async Task<IEnumerable<ExpertTopic>> ListAsync()
        {
            return await _context.ExpertTopics
                .Include(et => et.Tutor)
                .Include(et => et.Topic)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpertTopic>> ListByTopicIdAsync(int topicId)
        {
            return await _context.ExpertTopics
                .Where(et => et.TopicId == topicId)
                .Include(et => et.Tutor)
                .Include(et => et.Topic)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExpertTopic>> ListByTutorIdAsync(int tutorId)
        {
            return await _context.ExpertTopics
                .Where(et => et.TutorId == tutorId)
                .Include(et => et.Tutor)
                .Include(et => et.Topic)
                .ToListAsync();
        }

        public void Remove(ExpertTopic expertTopic)
        {
            _context.ExpertTopics.Remove(expertTopic);
        }

        public async Task UnassignExpertTopic(int tutortId, int topicId)
        {
            ExpertTopic expertTopic = await FindByTutorIdAndTopicId(tutortId, topicId);
            if (expertTopic != null)
                Remove(expertTopic);
        }
    }
}
