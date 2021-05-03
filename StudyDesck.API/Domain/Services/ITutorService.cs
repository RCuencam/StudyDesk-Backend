using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ITutorService
    {
        Task<IEnumerable<Tutor>> ListAsync();
        Task<IEnumerable<Tutor>> ListByTopicIdAsync(int topicId);
        Task<TutorResponse> GetByIdAsync(int id);
        Task<TutorResponse> SaveAsync(Tutor tutor);
        Task<TutorResponse> UpdateAsync(int id, Tutor tutor);
        Task<TutorResponse> DeleteAsync(int id);
    }
}
