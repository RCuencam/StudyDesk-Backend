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
    public class SessionMaterialRepository : BaseRepository, ISessionMaterialRepository
    {
        public SessionMaterialRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SessionMaterial sessionMaterial)
        {
            await _context.SessionMaterials.AddAsync(sessionMaterial);
        }

        public async Task AssignSessionMaterial(int sessionId, long materialId, int tutorId)
        {
            SessionMaterial result = await FindBySessionIdAndStudyMaterial(sessionId, materialId);
            if(result == null)
            {
                result = new SessionMaterial
                {
                    SessionId = sessionId,
                    StudyMaterialId = materialId,
                    TutorId = tutorId
                };
                await AddAsync(result);
            }
        }

        public async Task AssignSessionMaterial(int sessionId, long materialId)
        {
            SessionMaterial result = await FindBySessionIdAndStudyMaterial(sessionId, materialId);
            if(result == null)
            {
                result = new SessionMaterial
                {
                    SessionId = sessionId,
                    StudyMaterialId = materialId
                };
                await AddAsync(result);
            }
            
        }

        public async Task UnassignStudyMaterial(int sessionId, long materialId)
        {
            SessionMaterial sessionMaterial = await FindBySessionIdAndStudyMaterial(sessionId, materialId);
            if (sessionMaterial != null)
                Remove(sessionMaterial);
        }


        //public async Task AddAsync(SessionMaterial sessionMaterial)
        //{
        //    await _context.SessionMaterials.AddAsync(sessionMaterial);
        //}

        //public async Task<SessionMaterial> FindById(int id)
        //{
        //    return await _context.SessionMaterials.FindAsync(id);
        //}

        public async Task<IEnumerable<SessionMaterial>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.SessionMaterials
                .Where(sm => sm.SessionId == sessionId)
                .Include(sm => sm.Session)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionMaterial>> ListByStudyMaterialIdAsync(int studyMaterialId)
        {
            return await _context.SessionMaterials
                .Where(sm => sm.StudyMaterialId == studyMaterialId)
                .Include(sm => sm.StudyMaterial)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionMaterial>> ListByTutorIdAsync(int tutorId)
        {
            return await _context.SessionMaterials
                .Where(sm => sm.TutorId == tutorId)
                .Include(sm => sm.Tutor)
                .ToListAsync();
        }

        public void Remove(SessionMaterial sessionMaterial)
        {
            _context.SessionMaterials.Remove(sessionMaterial);
        }

        public async Task<SessionMaterial> FindBySessionIdAndStudyMaterial(int sessionId, long studyMaterial)
        {
            return await _context.SessionMaterials.FindAsync(sessionId, studyMaterial);
        }
    }
}
