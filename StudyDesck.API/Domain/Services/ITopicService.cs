using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>> ListAsync();
        Task<TopicResponse> GetByIdAsync(int id);
        Task<TopicResponse> SaveAsync(Topic topic);
        Task<TopicResponse> UpdateAsync(int id, Topic topic);
        Task<TopicResponse> DeleteAsync(int id);
    }
}
