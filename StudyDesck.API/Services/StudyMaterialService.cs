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
    public class StudyMaterialService : IStudyMaterialService
    {
        private readonly IStudyMaterialRepository _studyMaterialRepository;
        private readonly IUnitOfWork _unitOfWork;
        public StudyMaterialService(IStudyMaterialRepository repository, IUnitOfWork unitOfWork)
        {
            _studyMaterialRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<StudyMaterialResponse> DeleteAsync(int id)
        {
            var existingStudyMaterial = await _studyMaterialRepository.FindById(id);
            if (existingStudyMaterial == null)
                return new StudyMaterialResponse("Study material not found");
            try
            {
                _studyMaterialRepository.Remove(existingStudyMaterial);
                await _unitOfWork.CompleteAsync();
                return new StudyMaterialResponse(existingStudyMaterial);
            }
            catch (Exception e)
            {
                return new StudyMaterialResponse("Has ocurred an error deleting the study material" + e.Message);
            }
        }

        public async Task<StudyMaterialResponse> GetByIdAsync(int id)
        {
            var existingStudyMaterial = await _studyMaterialRepository.FindById(id);
            if (existingStudyMaterial == null)
                return new StudyMaterialResponse("Study material not found");
            return new StudyMaterialResponse(existingStudyMaterial);
        }

        public async Task<IEnumerable<StudyMaterial>> ListAsync()
        {
            return await _studyMaterialRepository.ListAsync();
        }

        public async Task<StudyMaterialResponse> SaveAsync(StudyMaterial studyMaterial)
        {
            try
            {
                await _studyMaterialRepository.AddAsync(studyMaterial);
                await _unitOfWork.CompleteAsync();
                return new StudyMaterialResponse(studyMaterial);
            }
            catch (Exception e)
            {
                return new StudyMaterialResponse("Has ocurred an error saving the study material" + e.Message);
            }
        }

        public async Task<StudyMaterialResponse> UpdateAsync(int id, StudyMaterial studyMaterial)
        {
            var existingStudyMaterial = await _studyMaterialRepository.FindById(id);
            if (existingStudyMaterial == null)
                return new StudyMaterialResponse("Study material not found");

            existingStudyMaterial.Title = studyMaterial.Title;
            existingStudyMaterial.Description = studyMaterial.Description;
            try
            {
                _studyMaterialRepository.Update(existingStudyMaterial);
                await _unitOfWork.CompleteAsync();
                return new StudyMaterialResponse(existingStudyMaterial);
            }
            catch (Exception e)
            {
                return new StudyMaterialResponse("Has ocurred an error updating the study material" + e.Message);
            }
        }
    }
}
