using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> ListAsync();
        Task AddAsync(Student student);
        Task<Student> FindById(int id);
        void Update(Student student);
        void Remove(Student student);
    }
}
