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
        private readonly ITopicRepository _topicRepository;
        private readonly IUnitOfWork _unitOfWork;
        public StudyMaterialService(IStudyMaterialRepository repository, IUnitOfWork unitOfWork, ITopicRepository topicRepository)
        {
            _studyMaterialRepository = repository;
            _unitOfWork = unitOfWork;
            _topicRepository = topicRepository;
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

        public async Task<IEnumerable<StudyMaterial>> ListByStudentIdAsync(int studentId)
        {
            return await _studyMaterialRepository.ListByStudentIdAsync(studentId);
        }

        public async Task<IEnumerable<StudyMaterial>> ListByTopicIdAsync(int topicId)
        {
            return await _studyMaterialRepository.ListByTopicIdAsync(topicId);
        }

        public async Task<StudyMaterialResponse> SaveAsync(int topicId, StudyMaterial studyMaterial)
        {
            var existingTopic = await _topicRepository.FindById(topicId);

            if (existingTopic == null)
                return new StudyMaterialResponse("Topic Id not found");

            try
            {
                studyMaterial.TopicId = topicId;
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
            existingStudyMaterial.FileName = studyMaterial.FileName;
            existingStudyMaterial.FilePath = studyMaterial.FilePath;
            existingStudyMaterial.Size = studyMaterial.Size;
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
