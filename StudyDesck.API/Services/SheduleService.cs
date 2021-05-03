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
    public class SheduleService : ISheduleService
    {
        private readonly ISheduleRepository _sheduleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SheduleService(ISheduleRepository sheduleRepository, IUnitOfWork unitOfWork)
        {
            _sheduleRepository = sheduleRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<SheduleResponse> DeleteAsync(int id)
        {
            var existingShedule = await _sheduleRepository.FindById(id);

            if (existingShedule == null)
                return new SheduleResponse("Shedule not found");

            try
            {
                _sheduleRepository.Remove(existingShedule);
                await _unitOfWork.CompleteAsync();

                return new SheduleResponse(existingShedule);
            }
            catch (Exception ex)
            {
                return new SheduleResponse($"An error ocurred while deleting shedule: {ex.Message}");
            }
        }

        public async Task<SheduleResponse> GetByIdAsync(int id)
        {
            var existingShedule = await _sheduleRepository.FindById(id);
            if (existingShedule == null)
                return new SheduleResponse("Shedule not found");
            return new SheduleResponse(existingShedule);
        }

        public async Task<IEnumerable<Shedule>> ListAsync()
        {
            return await _sheduleRepository.ListAsync();
        }

        //public async Task<IEnumerable<Shedule>> ListByTutorIdAsync(int tutorId)
        //{
        //    return await _sheduleRepository.ListByTutorIdAsync(tutorId);
        //}
        public async Task<SheduleResponse> SaveAsync(Shedule shedule)
        {
            try
            {
                await _sheduleRepository.AddAsync(shedule);
                await _unitOfWork.CompleteAsync();

                return new SheduleResponse(shedule);
            }
            catch (Exception ex)
            {
                return new SheduleResponse($"An error ocurred while saving the shedule: {ex.Message}");
            }
        }

        public async Task<SheduleResponse> UpdateAsync(int id, Shedule shedule)
        {
            var existingShedule = await _sheduleRepository.FindById(id);

            if (existingShedule == null)
                return new SheduleResponse("Shedule not found");

            existingShedule.StarDate = shedule.StarDate;
            existingShedule.EndDate = shedule.EndDate;
            existingShedule.Date = shedule.Date;

            try
            {
                _sheduleRepository.Update(existingShedule);
                await _unitOfWork.CompleteAsync();

                return new SheduleResponse(existingShedule);
            }
            catch (Exception ex)
            {
                return new SheduleResponse($"An error ocurred while updating the shedule: {ex.Message}");
            }
        }
    }
}
