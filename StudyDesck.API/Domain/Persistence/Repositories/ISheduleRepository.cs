using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ISheduleRepository
    {
        Task<IEnumerable<Schedule>> ListAsync();
        //Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId);
        Task AddAsync(Schedule shedule);
        Task<Schedule> FindById(int id);
        void Update(Schedule shedule);
        void Remove(Schedule shedule);
    }
}
