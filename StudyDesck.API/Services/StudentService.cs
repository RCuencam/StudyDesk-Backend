using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net;

namespace StudyDesck.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly ISessionReservationRepository _sessionReservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, ISessionReservationRepository sessionReservationRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            //_careerRepository = careerRepository;
            _sessionReservationRepository = sessionReservationRepository;
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
            var list = await _studentRepository.ListAsync();
            /*foreach (var item in list)
            {
                item.Career = await _careerRepository.FindById(item.CareerId);
            }*/
            return list;
        }

        public async Task<IEnumerable<Student>> ListBySessionIdAsync(int sessionId)
        {
            var sessionReservations = await _sessionReservationRepository.ListBySessionIdAsync(sessionId);
            var students = sessionReservations.Select(sr => sr.Student).ToList();
            return students;
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
        public async Task<StudentResponse> SaveAsync(int careerId,Student student)
        {
            try
            {
                student.CareerId = careerId;
                student.Password = BCryptNet.BCrypt.HashPassword(student.Password);
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
            existingStudent.LastName = student.LastName;
            existingStudent.Logo = student.Logo;
            student.Email = student.Email;
            existingStudent.Password = BCryptNet.BCrypt.HashPassword(student.Password);

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
