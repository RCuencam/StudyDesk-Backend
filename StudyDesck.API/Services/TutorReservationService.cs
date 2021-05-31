
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
        private readonly IStudentRepository _studentRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly IPlatformRepository _platformRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TutorReservationService(ITutorReservationRepository tutorReservationRespository, IStudentRepository studentRepository, ITutorRepository tutorRepository, IPlatformRepository platformRepository, IUnitOfWork unitOfWork)
        {
            _tutorReservationRespository = tutorReservationRespository;
            _studentRepository = studentRepository;
            _tutorRepository = tutorRepository;
            _platformRepository = platformRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TutorReservationResponse> AssignTutorReservationAsync(int studentId, int tutorId, int platformId, TutorReservation tutorReservation)
        {
            var student = await _studentRepository.FindById(studentId);
            var tutor = await _tutorRepository.FindById(tutorId);
            var platform = await _platformRepository.FindById(platformId);

            if (student == null || tutor == null || platform==null)
                return new TutorReservationResponse("StudenId or TutorId or PlatformId not found");

            var existsIt = await _tutorReservationRespository.FindByStudentIdAndTutorIdAndPlatformId(studentId, tutorId, platformId);
            if (existsIt != null)
                return new TutorReservationResponse("This tutor reservation already exist");

            try
            {
                tutorReservation.Student = student;
                tutorReservation.Tutor = tutor;
                tutorReservation.Platform = platform;
                tutorReservation.StudentId = studentId;
                tutorReservation.TutorId = tutorId;
                tutorReservation.PlatformId = platformId;

                await _tutorReservationRespository.AddAsync(tutorReservation);
                await _unitOfWork.CompleteAsync();
                return new TutorReservationResponse(tutorReservation);
            }
            catch (Exception e)
            {
                return new TutorReservationResponse($"Ocurrió un error: {e.Message}");
            }
        }

        public Task<TutorReservationResponse> GetByPlatformId(int platformId)
        {
            throw new NotImplementedException();
        }

        public Task<TutorReservationResponse> GetByStudentId(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task<TutorReservationResponse> GetByStudentIdAndTutorIdAndPlatformId(int studentId, int tutorId, int platformId)
        {
            var existingTutorReserv = await _tutorReservationRespository.FindByStudentIdAndTutorIdAndPlatformId(studentId, tutorId, platformId);
            if (existingTutorReserv == null)
                return new TutorReservationResponse("This tutor reservation is not found");
            return new TutorReservationResponse(existingTutorReserv);
        }

        public Task<TutorReservationResponse> GetByTutorId(int tutorId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TutorReservation>> ListAsync()
        {
            return await _tutorReservationRespository.ListAsync();
        }

        public async Task<TutorReservationResponse> UnassignTutorReservationAsync(int studentId, int tutorId, int platformId)
        {
            var existingTutorReserv = await _tutorReservationRespository.FindByStudentIdAndTutorIdAndPlatformId(studentId, tutorId, platformId);
            if (existingTutorReserv == null)
                return new TutorReservationResponse("TutorReservation not found");

            try
            {
                _tutorReservationRespository.Remove(existingTutorReserv);
                await _unitOfWork.CompleteAsync();
                return new TutorReservationResponse(existingTutorReserv);
            }
            catch (Exception e)
            {
                return new TutorReservationResponse($"Ocurrió un error: {e.Message}");
            }
        }

        public async Task<TutorReservationResponse> UpdateTutorReservationAsync(int studentId, int tutorId, int platformId, TutorReservation tutorReservation)
        {
            var existingTutorReserv = await _tutorReservationRespository.FindByStudentIdAndTutorIdAndPlatformId(studentId, tutorId, platformId);
            if (existingTutorReserv == null)
                return new TutorReservationResponse("TutorReservation not found");

            try
            {
                existingTutorReserv.Qualification = tutorReservation.Qualification;
                existingTutorReserv.StartDateTime = tutorReservation.StartDateTime;
                existingTutorReserv.TotalPrice = tutorReservation.TotalPrice;

                _tutorReservationRespository.Update(existingTutorReserv);
                await _unitOfWork.CompleteAsync();
                return new TutorReservationResponse(existingTutorReserv);
            }
            catch (Exception e)
            {
                return new TutorReservationResponse($"Ocurrió un error: {e.Message}");
            }
        }
    }
}
