using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface IStudentMaterialRepository
    {
        Task<IEnumerable<StudentMaterial>> ListByStudentIdAsync(int studentId);
        Task<IEnumerable<StudentMaterial>> ListByCategoryIdAsync(int categoryId);
        Task<IEnumerable<StudentMaterial>> ListByInstituteIdAsync(int instituteId);
        Task<StudentMaterial> FindByStudentIdAndStudyMaterialId(int studentId, int studyMaterial);
        Task AddAsync(StudentMaterial studentMaterial);
        void Remove(StudentMaterial studentMaterial);

        Task AssignStudentMaterial(int studentId, int materialId, int categoryId, int instituteId); // TODO to see
        Task AssignStudentMaterial(int studentId, int materialId); // TODO to see
        Task UnassignstudyMaterial(int studentId, int materialId); // TODO to see
    }
}
