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
        //private readonly IuniversityRepository _universityRepository;
        //private readonly IuniversityRepository _universityRepository;
        private readonly IUnitOfWork _unitOfWork;

       
        public CareerService(ICareerRepository careerRepository, IUnitOfWork unitOfWork)
        {
            _careerRepository = careerRepository;
            //_universityRepository = universityRepository;
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

        public async Task<IEnumerable<Career>> FindByuniversityId(int universityId)
        {
            var list = await _careerRepository.FindByuniversityIdAsync(universityId);
            return list;
        }

        public async Task<IEnumerable<Career>> GetByuniversityIdAndCareerId(int universityId, int careerId)
        {
            var list = await _careerRepository.FindByuniversityIdAndCareerId(universityId, careerId);
            return list;
        }

        public async Task<IEnumerable<Career>> ListAsync()
        {
            var list = await _careerRepository.ListAsync();
            /*foreach (var item in list)
            {
                item.university = await _universityRepository.FindById(item.universityId);
            }*/
            return list;

            //return await _careerRepository.ListAsync();
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

        public async Task<CareerResponse> SaveAsync(int universityId,Career career)
        {
            try
            {
                career.universityId = universityId;
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
            existingCareer.universityId = career.universityId;
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
