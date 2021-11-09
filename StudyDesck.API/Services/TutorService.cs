using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net;

namespace StudyDesck.API.Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _tutorRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExpertTopicRepository _expertTopicRepository;

        public TutorService(ITutorRepository guardianRepository, IUnitOfWork unitOfWork, IExpertTopicRepository expertTopicRepository, ICourseRepository courseRepository)
        {
            _tutorRepository = guardianRepository;
            _unitOfWork = unitOfWork;
            _expertTopicRepository = expertTopicRepository;
            _courseRepository = courseRepository;
        }

        public async Task<TutorResponse> DeleteAsync(int id)
        {
            var existingTutor = await _tutorRepository.FindById(id);

            if (existingTutor == null)
                return new TutorResponse("Tutor not found");

            try
            {
                _tutorRepository.Remove(existingTutor);
                await _unitOfWork.CompleteAsync();

                return new TutorResponse(existingTutor);
            }
            catch (Exception ex)
            {
                return new TutorResponse($"An error ocurred while deleting the tutor: {ex.Message}");
            }
        }

        public async Task<TutorResponse> GetByIdAsync(int id)
        {
            var existingTutor = await _tutorRepository.FindById(id);

            if (existingTutor == null)
                return new TutorResponse("Tutor not found");
            return new TutorResponse(existingTutor);
        }

        public async Task<IEnumerable<Tutor>> ListAsync()
        {
            return await _tutorRepository.ListAsync();
        }

        public async Task<IEnumerable<Tutor>> ListByTopicIdAsync(int topicId)
        {
            var expertTopics = await _expertTopicRepository.ListByTopicIdAsync(topicId);
            var tutors = expertTopics.Select(et => et.Tutor).ToList();
            return tutors;
        }

        public async Task<TutorResponse> SaveAsync(int courseId, Tutor tutor)
        {
            try
            {
                tutor.CourseId = courseId;
                tutor.Password = BCryptNet.BCrypt.HashPassword(tutor.Password);
                await _tutorRepository.AddAsync(tutor);
                await _unitOfWork.CompleteAsync();

                return new TutorResponse(tutor);
            }
            catch (Exception ex)
            {
                return new TutorResponse($"An error ocurred while saving the tutor: {ex.Message}");
            }
        }

        public async Task<TutorResponse> UpdateAsync(int id, Tutor tutor)
        {
            var existingTutor = await _tutorRepository.FindById(id);

            if (existingTutor == null)
                return new TutorResponse("Tutor not found");

            existingTutor.Name = tutor.Name;
            existingTutor.LastName = tutor.LastName;
            existingTutor.Description = tutor.Description;
            existingTutor.Logo = tutor.Logo;
            existingTutor.Email = tutor.Email;
            existingTutor.Password = BCryptNet.BCrypt.HashPassword(tutor.Password);
            existingTutor.PricePerHour = tutor.PricePerHour;

            try
            {
                _tutorRepository.Update(existingTutor);
                await _unitOfWork.CompleteAsync();

                return new TutorResponse(existingTutor);
            }
            catch (Exception ex)
            {
                return new TutorResponse($"An error ocurred while updating the tutor: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Tutor>> ListByCourseIdAsync(int courseId)
        {
            return await _tutorRepository.ListByCourseIdAsync(courseId);
        }

        // Deprecated
        public async Task<TutorResponse> SaveAsync(Tutor tutor)
        {
            try
            {
                // serializar password
                await _tutorRepository.AddAsync(tutor);
                await _unitOfWork.CompleteAsync();

                return new TutorResponse(tutor);
            }
            catch (Exception ex)
            {
                return new TutorResponse($"An error ocurred while saving the tutor: {ex.Message}");
            }
        }

        public async Task<TutorResponse> GetByCourseIdandTutorIdAsync(int courseId, int tutorId)
        {
            var existingCourse = await _courseRepository.FindById(courseId);
            if (existingCourse == null)
            {
                return new TutorResponse("CourseId not found");
            }

            var existingTutor = await _tutorRepository.FindById(tutorId);

            if (existingTutor == null)
                return new TutorResponse("Tutor not found");

            return new TutorResponse(existingTutor);

        }
    }
}
