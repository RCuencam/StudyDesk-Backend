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
    public class CareerService : ICareerService
    {
        private readonly ICareerRepository _careerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CareerService(ICareerRepository repository, IUnitOfWork unitOfWork)
        {
            _careerRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CareerResponse> DeleteAsync(int id)
        {
            var existingCareer = await _careerRepository.FindById(id);
            if (existingCareer == null)
                return new CareerResponse("Career not found");
            try
            {
                _careerRepository.Remove(existingCareer);
                await _unitOfWork.CompleteAsync();
                return new CareerResponse(existingCareer);
            }
            catch (Exception e)
            {
                return new CareerResponse("Has ocurred an error deleting the career " + e.Message);
            }
        }

        public async Task<CareerResponse> GetByIdAsync(int id)
        {
            var existingCareer = await _careerRepository.FindById(id);
            if (existingCareer == null)
                return new CareerResponse("Career not found");

            return new CareerResponse(existingCareer);
        }

        public async Task<IEnumerable<Career>> ListAsync()
        {
            return await _careerRepository.ListAsync();
        }

        public async Task<CareerResponse> SaveAsync(Career career)
        {
            try
            {
                await _careerRepository.AddAsync(career);
                await _unitOfWork.CompleteAsync();
                return new CareerResponse(career);
            }
            catch (Exception e)
            {
                return new CareerResponse("Has ocurred an error saving the career " + e.Message);
            }
        }

        public async Task<CareerResponse> UpdateAsync(int id, Career career)
        {
            var existingCareer = await _careerRepository.FindById(id);
            if (existingCareer == null)
                return new CareerResponse("category not found");

            existingCareer.Name = career.Name;
            try
            {
                _careerRepository.Update(existingCareer);
                await _unitOfWork.CompleteAsync();
                return new CareerResponse(existingCareer);
            }
            catch (Exception e)
            {
                return new CareerResponse("Has ocurred an error updating the career " + e.Message);
            }
        }
    }
}
