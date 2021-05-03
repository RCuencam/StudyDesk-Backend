using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IPlatformService
    {
        Task<IEnumerable<Platform>> ListAsync();
        Task<PlatformResponse> GetByIdAsync(int id);
        Task<PlatformResponse> SaveAsync(Platform category);
        Task<PlatformResponse> UpdateAsync(int id, Platform category);
        Task<PlatformResponse> DeleteAsync(int id);
    }
}
