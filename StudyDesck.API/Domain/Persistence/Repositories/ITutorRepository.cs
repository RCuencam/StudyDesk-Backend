using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ITutorRepository
    {
        Task<IEnumerable<Tutor>> ListAsync();
        Task<IEnumerable<Tutor>> ListByCareerIdAsync(int careerId);
        Task<Tutor> FindById(int tutorId);
        Task AddAsync(Tutor tutor);
        void Update(Tutor tutor);
        void Remove(Tutor tutor);
    }
}
