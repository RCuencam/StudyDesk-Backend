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
    public class universityService : IUniversityService
    {
        private readonly IUniversityRepository _universityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public universityService(IUniversityRepository repository,IUnitOfWork unitOfWork)
        {
            _universityRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UniversityResponse> DeleteAsync(int id)
        {
            var existinguniversity = await _universityRepository.FindById(id);
            if (existinguniversity == null)
                return new UniversityResponse("university not found");
            try
            {
                _universityRepository.Remove(existinguniversity);
                await _unitOfWork.CompleteAsync();
                return new UniversityResponse(existinguniversity);
            }
            catch (Exception e)
            {
                return new UniversityResponse("Has ocurred an error deleting the university " + e.Message);
            }
        }

        public async Task<UniversityResponse> GetByIdAsync(int id)
        {
            var existinguniversity = await _universityRepository.FindById(id);
            if (existinguniversity == null)
                return new UniversityResponse("university not found");

            return new UniversityResponse(existinguniversity);
        }

        public async Task<IEnumerable<University>> ListAsync()
        {
            return await _universityRepository.ListAsync();
        }

        public async Task<UniversityResponse> SaveAsync(University university)
        {
            try
            {
                await _universityRepository.AddAsync(university);
                await _unitOfWork.CompleteAsync();
                return new UniversityResponse(university);
            }
            catch (Exception e)
            {
                return new UniversityResponse("Has ocurred an error saving the university " + e.Message);
            }
        }

        public async Task<UniversityResponse> UpdateAsync(int id, University university)
        {
            var existinguniversity = await _universityRepository.FindById(id);
            if (existinguniversity == null)
                return new UniversityResponse("category no encontrada");

            existinguniversity.Name = university.Name;
            try
            {
                _universityRepository.Update(existinguniversity);
                await _unitOfWork.CompleteAsync();
                return new UniversityResponse(existinguniversity);
            }
            catch (Exception e)
            {
                return new UniversityResponse("Has ocurred an error updating the university " + e.Message);
            }
        }
    }
}
