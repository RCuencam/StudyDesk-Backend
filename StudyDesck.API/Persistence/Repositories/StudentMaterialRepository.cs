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
    public class StudentMaterialRepository : BaseRepository, IStudentMaterialRepository
    {
        public StudentMaterialRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(StudentMaterial studentMaterial)
        {
            await _context.studentMaterials.AddAsync(studentMaterial);
        }

        public async Task AssignStudentMaterial(int studentId, long materialId, int categoryId, int instituteId)
        {
            StudentMaterial result = await FindByStudentIdAndStudyMaterialId(studentId, materialId);
            if (result == null)
            {
                result = new StudentMaterial
                {
                    StudentId = studentId,
                    StudyMaterialId = materialId,
                    CategoryId = categoryId,
                    InstituteId = instituteId
                };
                await AddAsync(result);
            }
        }

        public async Task<StudentMaterial> FindByStudentIdAndStudyMaterialId(int studentId, long studyMaterial)
        {
            return await _context.studentMaterials.FindAsync(studentId, studyMaterial);
            
        }

        public Task<IEnumerable<StudentMaterial>> ListByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentMaterial>> ListByInstituteIdAsync(int instituteId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StudentMaterial>> ListByStudentIdAsync(int studentId)
        {
            return await _context.studentMaterials
                .Where(sm => sm.StudentId == studentId)
                .Include(sm => sm.StudyMaterial)
                .ToListAsync();
        }

        public void Remove(StudentMaterial studentMaterial)
        {
            _context.studentMaterials.Remove(studentMaterial);
        }

        public async Task UnassignstudyMaterial(int studentId, long materialId)
        {
            StudentMaterial studentMaterial = await FindByStudentIdAndStudyMaterialId(studentId, materialId);
            if (studentMaterial != null)
                Remove(studentMaterial);
        }
    }
}
