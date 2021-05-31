using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _tutorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TutorService(ITutorRepository guardianRepository, IUnitOfWork unitOfWork)
        {
            _tutorRepository = guardianRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<TutorResponse> SaveAsync(int careerId, Tutor tutor)
        {
            try
            {
                tutor.CareerId = careerId;
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
                return new TutorResponse("Guardian not found");

            existingTutor.Name = tutor.Name;
            existingTutor.LastName = tutor.LastName;
            existingTutor.Description = tutor.Description;
            existingTutor.Logo = tutor.Logo;
            existingTutor.Email = tutor.Email;
            existingTutor.Password = tutor.Password;
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

        public async Task<IEnumerable<Tutor>> ListByCareerIdAsync(int careerId)
        {
            return await _tutorRepository.ListByCareerIdAsync(careerId);
        }
    }
}
