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
        private readonly IUnitOfWork _unitOfWork;

        public StudentMaterialService(IStudentMaterialRepository studentMaterialRepository, IUnitOfWork unitOfWork)
        {
            _studentMaterialRepository = studentMaterialRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentMaterial>> ListByStudentIdAsync(int studentId)
        {
            return await _studentMaterialRepository.ListByStudentIdAsync(studentId);
        }

        public async Task<StudentMaterialResponse> AssignStudentMaterialAsync
            (int studentId, long materialId, int categoryId, int instituteId)
        {
            try
            {
                await _studentMaterialRepository.AssignStudentMaterial(studentId, materialId, categoryId, instituteId);
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

        public async Task<StudentMaterialResponse> UnassignStudentMaterialAsync(int studentId, long materialId)
        {
            try
            {
                StudentMaterial studentMaterial = 
                    await _studentMaterialRepository.FindByStudentIdAndStudyMaterialId(studentId, materialId);

                _studentMaterialRepository.Remove(studentMaterial);
                await _unitOfWork.CompleteAsync();

                return new StudentMaterialResponse(studentMaterial);

            }
            catch (Exception ex)
            {
                return new StudentMaterialResponse
                    ($"An error ocurred while unassigning StudyMaterial from Student: {ex.Message}");
            }
        
        }


    }
}
