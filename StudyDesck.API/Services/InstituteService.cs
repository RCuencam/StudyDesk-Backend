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
    public class InstituteService : IInstituteService
    {
        private readonly IInstituteRepository _instituteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InstituteService(IInstituteRepository repository,IUnitOfWork unitOfWork)
        {
            _instituteRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<InstituteResponse> DeleteAsync(int id)
        {
            var existingInstitute = await _instituteRepository.FindById(id);
            if (existingInstitute == null)
                return new InstituteResponse("Institute not found");
            try
            {
                _instituteRepository.Remove(existingInstitute);
                await _unitOfWork.CompleteAsync();
                return new InstituteResponse(existingInstitute);
            }
            catch (Exception e)
            {
                return new InstituteResponse("Has ocurred an error deleting the institute " + e.Message);
            }
        }

        public async Task<InstituteResponse> GetByIdAsync(int id)
        {
            var existingInstitute = await _instituteRepository.FindById(id);
            if (existingInstitute == null)
                return new InstituteResponse("Institute not found");

            return new InstituteResponse(existingInstitute);
        }

        public async Task<IEnumerable<Institute>> ListAsync()
        {
            return await _instituteRepository.ListAsync();
        }

        public async Task<InstituteResponse> SaveAsync(Institute institute)
        {
            try
            {
                await _instituteRepository.AddAsync(institute);
                await _unitOfWork.CompleteAsync();
                return new InstituteResponse(institute);
            }
            catch (Exception e)
            {
                return new InstituteResponse("Has ocurred an error saving the institute " + e.Message);
            }
        }

        public async Task<InstituteResponse> UpdateAsync(int id, Institute institute)
        {
            var existingInstitute = await _instituteRepository.FindById(id);
            if (existingInstitute == null)
                return new InstituteResponse("category no encontrada");

            existingInstitute.Name = institute.Name;
            try
            {
                _instituteRepository.Update(existingInstitute);
                await _unitOfWork.CompleteAsync();
                return new InstituteResponse(existingInstitute);
            }
            catch (Exception e)
            {
                return new InstituteResponse("Has ocurred an error updating the institute " + e.Message);
            }
        }
    }
}
