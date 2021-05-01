using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> ListAsync();
        Task<StudentResponse> GetByIdAsync(int id);
        Task<StudentResponse> SaveAsync(Student student);
        Task<StudentResponse> UpdateAsync(int id, Student student);
        Task<StudentResponse> DeleteAsync(int id);
    }
}
