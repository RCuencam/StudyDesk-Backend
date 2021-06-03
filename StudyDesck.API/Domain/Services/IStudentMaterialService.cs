using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IStudentMaterialService
    {
        Task<IEnumerable<StudentMaterial>> ListByStudentIdAsync(int studentId);
        Task<StudentMaterialResponse> AssignStudentMaterialAsync(int studentId, long materialId);
        Task<StudentMaterialResponse> UnassignStudentMaterialAsync(int studentId, long materialId);
        Task<StudentMaterialResponse> AssignStudentMaterialAsync(int studentId, StudentMaterial studentMaterial);
    }
}
