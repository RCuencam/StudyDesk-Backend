using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IInstituteService
    {
        Task<IEnumerable<Institute>> ListAsync();
        Task<InstituteResponse> GetByIdAsync(int id);
        Task<InstituteResponse> SaveAsync(Institute institute);
        Task<InstituteResponse> UpdateAsync(int id, Institute institute);
        Task<InstituteResponse> DeleteAsync(int id);
    }
}
