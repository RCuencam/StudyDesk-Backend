using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> ListAsync();
        Task<IEnumerable<Course>> ListByCareerIdAsync(int careerId);
        Task AddAsync(Course course);
        Task<Course> FindById(int id);
        void Update(Course course);
        void Remove(Course course);
    }
}
