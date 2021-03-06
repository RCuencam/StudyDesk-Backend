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
        Task<IEnumerable<Tutor>> ListByCourseIdAsync(int courseId);
        Task<IEnumerable<Tutor>> ListByTopicIdAsync(int topicId);
        Task<TutorResponse> GetByIdAsync(int tutorId);
        Task<TutorResponse> GetByCourseIdandTutorIdAsync(int courseId, int tutorId);
        Task<TutorResponse> SaveAsync(int courseId, Tutor tutor);
        Task<TutorResponse> SaveAsync(Tutor tutor);

        Task<TutorResponse> UpdateAsync(int tutorId, Tutor tutor);
        Task<TutorResponse> DeleteAsync(int tutorId);
    }
}
