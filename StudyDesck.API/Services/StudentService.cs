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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository repository, IUnitOfWork unitOfWork)
        {
            _studentRepository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<StudentResponse> DeleteAsync(int id)
        {
            var existingStudent = await _studentRepository.FindById(id);
            if (existingStudent == null)
                return new StudentResponse("Student not found");
            try
            {
                _studentRepository.Remove(existingStudent);
                await _unitOfWork.CompleteAsync();
                return new StudentResponse(existingStudent);
            }
            catch (Exception e)
            {
                return new StudentResponse("Has ocurred an error deleting the student " + e.Message);
            }
        }

        public async Task<StudentResponse> GetByIdAsync(int id)
        {
            var existingStudent = await _studentRepository.FindById(id);
            if (existingStudent == null)
                return new StudentResponse("Student not found");

            return new StudentResponse(existingStudent);
        }

        public async Task<IEnumerable<Student>> ListAsync()
        {
            return await _studentRepository.ListAsync();
        }

        public async Task<StudentResponse> SaveAsync(Student student)
        {
            try
            {
                await _studentRepository.AddAsync(student);
                await _unitOfWork.CompleteAsync();
                return new StudentResponse(student);
            }
            catch (Exception e)
            {
                return new StudentResponse("Has ocurred an error saving the student " + e.Message);
            }
        }

        public async Task<StudentResponse> UpdateAsync(int id, Student student)
        {
            var existingStudent = await _studentRepository.FindById(id);
            if (existingStudent == null)
                return new StudentResponse("student no encontrada");

            existingStudent.Name = student.Name;
            try
            {
                _studentRepository.Update(existingStudent);
                await _unitOfWork.CompleteAsync();
                return new StudentResponse(existingStudent);
            }
            catch (Exception e)
            {
                return new StudentResponse("Has ocurred an error updating the student " + e.Message);
            }
        }
    }
}
