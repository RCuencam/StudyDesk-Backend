using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface IExpertTopicService
    {
        Task<IEnumerable<ExpertTopic>> ListAsync();
        Task<IEnumerable<ExpertTopic>> ListByTutorIdAsync(int tutorId);
        Task<IEnumerable<ExpertTopic>> ListByTopicIdAsync(int topicId);
        Task<ExpertTopicResponse> AssignExpertTopicAsync(int tutorId, int topicId);
        Task<ExpertTopicResponse> UnassignExpertTopicAsync(int tutorId, int topicId);
    }
}
