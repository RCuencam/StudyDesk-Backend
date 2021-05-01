using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface IInstituteRepository
    {
        Task<IEnumerable<Institute>> ListAsync();
        Task AddAsync(Institute institute);
        Task<Institute> FindById(int id);
        void Update(Institute institute);
        void Remove(Institute institute);
    }
}
