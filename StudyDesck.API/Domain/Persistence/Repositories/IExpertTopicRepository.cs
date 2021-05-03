using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Persistence.Repositories
{
    public interface IExpertTopicRepository
    {
        Task<IEnumerable<ExpertTopic>> ListAsync();
        Task<IEnumerable<ExpertTopic>> ListByTutorIdAsync(int tutorId);
        Task<IEnumerable<ExpertTopic>> ListByTopicIdAsync(int topicId);
        Task<ExpertTopic> FindByTutorIdAndTopicId(int tutorId, int topicId);
        Task AddAsync(ExpertTopic expertTopic);
        void Remove(ExpertTopic expertTopic);
        Task AssignExpertTopic(int tutorId, int topicId);
        Task UnassignExpertTopic(int tutortId, int topicId);
    }
}
