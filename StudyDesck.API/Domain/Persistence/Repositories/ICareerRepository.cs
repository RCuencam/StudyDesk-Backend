using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ICareerRepository
    {
        Task<IEnumerable<Career>> ListAsync();
        Task AddAsync(Career career);
        Task<Career> FindById(int id);

        Task<IEnumerable<Career>> FindByInstituteIdAndCareerId(int instituteId, int careerId);
        Task<IEnumerable<Career>> FindByInstituteIdAsync(int instituteId);
        void Update(Career career);
        void Remove(Career career);
    }
}
