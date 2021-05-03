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

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _context.Sessions.ToListAsync();
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
