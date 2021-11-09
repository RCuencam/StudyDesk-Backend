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
        Task<IEnumerable<Career>> FindByuniversityId(int universityId);
        Task<IEnumerable<Career>> GetByuniversityIdAndCareerId(int universityId,int careerId);
        Task<CareerResponse> SaveAsync(Career career);
        Task<CareerResponse> SaveAsync(int universityId, Career career);
        Task<CareerResponse> UpdateAsync(int id, Career career);
        Task<CareerResponse> DeleteAsync(int id);
    }
}
