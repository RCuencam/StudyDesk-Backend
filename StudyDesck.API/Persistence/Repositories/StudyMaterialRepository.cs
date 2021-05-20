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
    public class StudyMaterialRepository : BaseRepository, IStudyMaterialRepository
    {
        public StudyMaterialRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(StudyMaterial studyMaterial)
        {
            await _context.StudyMaterials.AddAsync(studyMaterial);
        }

        public async Task<StudyMaterial> FindById(int id)
        {
            return await _context.StudyMaterials.FindAsync(id);
        }

        public async Task<IEnumerable<StudyMaterial>> ListAsync()
        {
            return await _context.StudyMaterials.ToListAsync();
        }

        public void Remove(StudyMaterial studyMaterial)
        {
            _context.StudyMaterials.Remove(studyMaterial);
        }

        public void Update(StudyMaterial studyMaterial)
        {
            _context.StudyMaterials.Update(studyMaterial);
        }
    }
}
