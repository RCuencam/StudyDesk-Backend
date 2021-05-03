using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface IPlatformRepository
    {
        Task<IEnumerable<Platform>> ListAsync();
        Task AddAsync(Platform platform);
        Task<Platform> FindById(int id);
        void Update(Platform platform);
        void Remove(Platform platform);
    }
}
