using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IUniversityService
    {
        Task<IEnumerable<University>> ListAsync();
        Task<UniversityResponse> GetByIdAsync(int id);
        Task<UniversityResponse> SaveAsync(University university);
        Task<UniversityResponse> UpdateAsync(int id, University university);
        Task<UniversityResponse> DeleteAsync(int id);
    }
}
