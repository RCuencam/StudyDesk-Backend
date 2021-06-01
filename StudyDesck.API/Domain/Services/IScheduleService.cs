using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> ListAsync();
        Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId);
        Task<ScheduleResponse> GetByIdAsync(int id);
        Task<ScheduleResponse> SaveAsync(int tutorId, Schedule schedule);
        Task<ScheduleResponse> UpdateAsync(int id, Schedule schedule);
        Task<ScheduleResponse> DeleteAsync(int id);
    }
}
