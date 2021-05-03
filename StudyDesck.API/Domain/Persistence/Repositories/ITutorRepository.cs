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
        Task AddAsync(Tutor tutor);
        Task<Tutor> FindById(int id);
        void Update(Tutor tutor);
        void Remove(Tutor tutor);
    }
}
