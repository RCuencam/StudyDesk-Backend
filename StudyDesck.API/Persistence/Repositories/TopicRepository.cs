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
    public class TopicRepository : BaseRepository, ITopicRepository
    {
        public TopicRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Topic topic)
        {
            await _context.Topics.AddAsync(topic);
        }

        public async Task<Topic> FindById(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public async Task<IEnumerable<Topic>> ListAsync()
        {
            return await _context.Topics.ToListAsync();
        }

        public async Task<IEnumerable<Topic>> ListByCourseIdAsync(int courseId)
        {
            return await _context.Topics
                .Where(t => t.CourseId == courseId)
                .Include(t => t.Course)
                    .ThenInclude(c => c.Career)
                .ToListAsync();
        }

        public void Remove(Topic topic)
        {
            _context.Topics.Remove(topic);
        }

        public void Update(Topic topic)
        {
            _context.Topics.Update(topic);
        }
    }
}
