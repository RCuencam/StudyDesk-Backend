using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ISheduleRepository
    {
        Task<IEnumerable<Shedule>> ListAsync();
        //Task<IEnumerable<Shedule>> ListByTutorIdAsync(int tutorId);
        Task AddAsync(Shedule shedule);
        Task<Shedule> FindById(int id);
        void Update(Shedule shedule);
        void Remove(Shedule shedule);
    }
}
