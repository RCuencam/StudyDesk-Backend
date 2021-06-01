using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ICareerService
    {
        Task<IEnumerable<Career>> ListAsync();
        Task<CareerResponse> GetByIdAsync(int id);
        Task<IEnumerable<Career>> FindByInstituteId(int instituteId);

        Task<IEnumerable<Career>> GetByInstituteIdAndCareerId(int instituteId,int careerId);
        Task<CareerResponse> SaveAsync(Career career);
        Task<CareerResponse> SaveAsync(int instituteId, Career career);
        Task<CareerResponse> UpdateAsync(int id, Career career);
        Task<CareerResponse> DeleteAsync(int id);
    }
}
