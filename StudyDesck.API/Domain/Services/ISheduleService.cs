using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ISheduleService
    {
        Task<IEnumerable<Schedule>> ListAsync();
        //Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId);
        Task<SheduleResponse> GetByIdAsync(int id);
        Task<SheduleResponse> SaveAsync(Schedule shedule);
        Task<SheduleResponse> UpdateAsync(int id, Schedule shedule);
        Task<SheduleResponse> DeleteAsync(int id);
    }
}
