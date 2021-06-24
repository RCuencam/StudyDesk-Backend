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
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _sheduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScheduleService(IScheduleRepository sheduleRepository, IUnitOfWork unitOfWork)
        {
            _sheduleRepository = sheduleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ScheduleResponse> DeleteAsync(int sheduleId)
        {
            var existingShedule = await _sheduleRepository.FindById(sheduleId);

            if (existingShedule == null)
                return new ScheduleResponse("Schedule not found");

            try
            {
                _sheduleRepository.Remove(existingShedule);
                await _unitOfWork.CompleteAsync();

                return new ScheduleResponse(existingShedule);
            }
            catch (Exception ex)
            {
                return new ScheduleResponse($"An error ocurred while deleting shedule: {ex.Message}");
            }
        }

        public async Task<ScheduleResponse> GetByIdAsync(int sheduleId)
        {
            var existingShedule = await _sheduleRepository.FindById(sheduleId);
            if (existingShedule == null)
                return new ScheduleResponse("Schedule not found");
            return new ScheduleResponse(existingShedule);
        }

        public async Task<IEnumerable<Schedule>> ListByTutorIdAsync(int tutorId)
        {
            return await _sheduleRepository.ListByTutorIdAsync(tutorId);
        }
        public async Task<IEnumerable<Schedule>> ListAsync()
        {
            return await _sheduleRepository.ListAsync();
        }
        public async Task<ScheduleResponse> SaveAsync(int tutorId,Schedule shedule)
        {
            try
            {
                shedule.TutorId = tutorId;
                await _sheduleRepository.AddAsync(shedule);
                await _unitOfWork.CompleteAsync();
                return new ScheduleResponse("successfully created schedule!");
            }
            catch (Exception ex)
            {
                return new ScheduleResponse($"An error ocurred while saving the shedule: {ex.Message}");
            }
        }

        public async Task<ScheduleResponse> UpdateAsync(int sheduleId, Schedule shedule)
        {
            var existingShedule = await _sheduleRepository.FindById(sheduleId);

            if (existingShedule == null)
                return new ScheduleResponse("Schedule not found");

            existingShedule.StarDate = shedule.StarDate;
            existingShedule.EndDate = shedule.EndDate;

            try
            {
                _sheduleRepository.Update(existingShedule);
                await _unitOfWork.CompleteAsync();

                return new ScheduleResponse(existingShedule);
            }
            catch (Exception ex)
            {
                return new ScheduleResponse($"An error ocurred while updating the shedule: {ex.Message}");
            }
        }
    }
}
