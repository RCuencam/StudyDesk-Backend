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
    public class StudentMaterialService : IStudentMaterialService
    {
        private readonly IStudentMaterialRepository _studentMaterialRepository;
        private readonly IStudyMaterialRepository _studyMaterialRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentMaterialService
            (IStudentMaterialRepository studentMaterialRepository, IUnitOfWork unitOfWork, IStudyMaterialRepository studyMaterialRepository)
        {
            _studentMaterialRepository = studentMaterialRepository;
            _unitOfWork = unitOfWork;
            _studyMaterialRepository = studyMaterialRepository;
        }

        public async Task<IEnumerable<StudentMaterial>> ListByStudentIdAsync(int studentId)
        {
            return await _studentMaterialRepository.ListByStudentIdAsync(studentId);
        }

        // deprecated
        public async Task<StudentMaterialResponse> AssignStudentMaterialAsync(int studentId, int materialId)
        {
            try
            {
                await _studentMaterialRepository.AssignStudentMaterial(studentId, materialId);
                await _unitOfWork.CompleteAsync();
                StudentMaterial studentMaterial = 
                    await _studentMaterialRepository.FindByStudentIdAndStudyMaterialId(studentId, materialId);
                return new StudentMaterialResponse(studentMaterial);

            }
            catch (Exception ex)
            {
                return new StudentMaterialResponse
                    ($"An error ocurred while assigning StudyMaterial to Student: {ex.Message}");
            }
        }

        public async Task<StudentMaterialResponse> UnassignStudentMaterialAsync(int studentId, int materialId)
        {
            try
            {
                StudentMaterial studentMaterial = 
                    await _studentMaterialRepository.FindByStudentIdAndStudyMaterialId(studentId, materialId);

                _studentMaterialRepository.Remove(studentMaterial);
                await _unitOfWork.CompleteAsync();

                var material = await _studyMaterialRepository.FindById(materialId);
                 _studyMaterialRepository.Remove(material);
                await _unitOfWork.CompleteAsync();

                return new StudentMaterialResponse(studentMaterial);

            }
            catch (Exception ex)
            {
                return new StudentMaterialResponse
                    ($"An error ocurred while unassigning StudyMaterial from Student: {ex.Message}");
            }
        
        }

        public async Task<StudentMaterialResponse> AssignStudentMaterialAsync(int studentId, StudentMaterial studentMaterial)
        {
            try
            {
                var material = await _studyMaterialRepository.SaveAsync(studentMaterial.StudyMaterial);
                await _unitOfWork.CompleteAsync();
                await _studentMaterialRepository.AssignStudentMaterial(studentId,
                    material.Id,
                    studentMaterial.CategoryId, 
                    studentMaterial.InstituteId);

                await _unitOfWork.CompleteAsync();
                StudentMaterial result =
                    await _studentMaterialRepository.FindByStudentIdAndStudyMaterialId(studentId, material.Id);
                return new StudentMaterialResponse(result);

            }
            catch (Exception ex)
            {
                return new StudentMaterialResponse
                    ($"An error ocurred while assigning StudyMaterial to Student: {ex.Message}");
            }
        }
    }
}
