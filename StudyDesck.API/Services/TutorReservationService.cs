
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
    public class TutorReservationService : ITutorReservationService
    {
        private readonly ITutorReservationRepository _tutorReservationRespository;
        private readonly IUnitOfWork _unitOfWork;

        public TutorReservationService(ITutorReservationRepository tutorReservationRespository, IUnitOfWork unitOfWork)
        {
            _tutorReservationRespository = tutorReservationRespository;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<TutorReservation>> ListByStudentIdAsync(int studentId)
        {
            return await _tutorReservationRespository.ListByStudentIdAsync(studentId);
        }

        public async Task<IEnumerable<TutorReservation>> ListByTutorIdAsync(int tutorId)
        {
            return await _tutorReservationRespository.ListByTutorIdAsync(tutorId);
        }

        public async Task<IEnumerable<TutorReservation>> ListTutorReservationByTutorIdAsync(int tutorId)
        {
            return await _tutorReservationRespository.ListAllByTutorIdAsync(tutorId);
        }

        public async Task<TutorReservationResponse> SaveTutorReservation(int studentId, int tutorId, TutorReservation tutorReservation)
        {
            try
            {
                tutorReservation.StudentId = studentId;
                tutorReservation.TutorId = tutorId;
                await _tutorReservationRespository.AddAsync(tutorReservation);
                await _unitOfWork.CompleteAsync();
                return new TutorReservationResponse("saved satisfactory!");
            }
            catch (Exception ex)
            {
                return new TutorReservationResponse
                    ($"An error ocurred while assigning the Tutor to Student: {ex.Message}");
            }
        }

        public async Task<TutorReservationResponse> UpdateTutorReservation(int id, int studentId, int tutorId, TutorReservation tutorReservation)
        {
            var existing = await _tutorReservationRespository.FindById(id, studentId, tutorId);
            if (existing == null)
                return new TutorReservationResponse("TutorReservation not found");

            existing.Qualification = tutorReservation.Qualification;
            existing.PlatformUrl = tutorReservation.PlatformUrl;
            existing.StartDateTime = tutorReservation.StartDateTime;
            existing.EndDateTime = tutorReservation.EndDateTime;
            existing.Description = tutorReservation.Description;
            existing.Confirmed = tutorReservation.Confirmed;
            
            try
            {
                _tutorReservationRespository.Update(existing);
                await _unitOfWork.CompleteAsync();
                return new TutorReservationResponse(existing);
            }
            catch (Exception e)
            {
                return new TutorReservationResponse("Has ocurred an error updating TutorReservation " + e.Message);
            }
        }
    }
}
