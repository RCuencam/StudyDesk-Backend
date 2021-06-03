using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface IStudyMaterialRepository
    {
        Task<IEnumerable<StudyMaterial>> ListAsync();
        Task AddAsync(StudyMaterial studyMaterial);
        Task<StudyMaterial> SaveAsync(StudyMaterial studyMaterial);

        Task<StudyMaterial> FindById(int id);
        void Update(StudyMaterial studyMaterial);
        void Remove(StudyMaterial studyMaterial);
        Task<IEnumerable<StudyMaterial>> ListByStudentIdAsync(int studentId);
    }
}
