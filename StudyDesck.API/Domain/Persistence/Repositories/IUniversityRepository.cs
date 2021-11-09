using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface IUniversityRepository
    {
        Task<IEnumerable<University>> ListAsync();
        Task AddAsync(University university);
        Task<University> FindById(int id);
        void Update(University university);
        void Remove(University university);
    }
}
