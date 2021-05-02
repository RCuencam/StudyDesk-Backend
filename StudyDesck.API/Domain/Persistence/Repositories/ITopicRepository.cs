using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> ListAsync();
        Task AddAsync(Topic topic);
        Task<Topic> FindById(int id);
        void Update(Topic topic);
        void Remove(Topic topic);
    }
}
