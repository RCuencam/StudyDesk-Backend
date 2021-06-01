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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CourseService(ICourseRepository repository, IUnitOfWork unitOfWork, ICareerRepository careerRepository)
        {
            _courseRepository = repository;
            _unitOfWork = unitOfWork;
            _careerRepository = careerRepository;
        }

        // deprecated
        public async Task<CourseResponse> DeleteAsync(int id)
        {
            var existingCourse = await _courseRepository.FindById(id);
            if (existingCourse == null)
                return new CourseResponse("Course not found");
            try
            {
                _courseRepository.Remove(existingCourse);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(existingCourse);
            } catch (Exception e)
            {
                return new CourseResponse("Has ocurred an error deleting the Course" + e.Message);
            }
        }

        public async Task<CourseResponse> DeleteAsync(int careerId, int id)
        {
            var existingCourse = await _courseRepository.FindById(id);
            if (existingCourse == null)
                return new CourseResponse("Course not found");
            if (existingCourse.CareerId != careerId)
                return new CourseResponse("Career not found for this Course");
            try
            {
                _courseRepository.Remove(existingCourse);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(existingCourse);
            }
            catch (Exception e)
            {
                return new CourseResponse("Has ocurred an error deleting the Course" + e.Message);
            }
        }

        // deprecated
        public async Task<CourseResponse> GetByIdAsync(int id)
        {
            var existingCourse = await _courseRepository.FindById(id);
            if (existingCourse == null)
                return new CourseResponse("course not found");
            return new CourseResponse(existingCourse);

        }

        public async Task<CourseResponse> GetByIdAsync(int careerId, int id) 
        {
            var existingCourse = await _courseRepository.FindById(id);
            if (existingCourse == null)
                return new CourseResponse("Course not found");
            if (existingCourse.CareerId != careerId)
                return new CourseResponse("Career not found for this Course");
            var career = await _careerRepository.FindById(careerId);
            existingCourse.Career = career;

            return new CourseResponse(existingCourse);
        }

        // deprecated
        public async Task<IEnumerable<Course>> ListAsync() 
        {
            return await _courseRepository.ListAsync();
        }

        public async Task<IEnumerable<Course>> ListByCareerIdAsync(int careerId)
        {
            return await _courseRepository.ListByCareerIdAsync(careerId);
        }

        // deprecated
        public async Task<CourseResponse> SaveAsync(Course course)
        {
            try
            {
                await _courseRepository.AddAsync(course);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(course);
            } catch (Exception e)
            {
                return new CourseResponse("Has ocurred an error saving the Course" + e.Message);
            }

        }

        public async Task<CourseResponse> SaveAsync(int careerId, Course course)
        {
            var existingCareer = await _careerRepository.FindById(careerId);
            if (existingCareer == null)
                return new CourseResponse("Career not found");

            try
            {
                course.CareerId = careerId;
                await _courseRepository.AddAsync(course);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(course);
            }
            catch (Exception e)
            {
                return new CourseResponse("Has ocurred an error saving the Course" + e.Message);
            }
        }

        // deprecated
        public async Task<CourseResponse> UpdateAsync(int id, Course course) 
        {
            var existingCourse = await _courseRepository.FindById(id);
            if (existingCourse == null)
                return new CourseResponse("Course not found");
            
            existingCourse.Name = course.Name;
            try
            {
                _courseRepository.Update(existingCourse);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(existingCourse);
            }catch(Exception e)
            {
                return new CourseResponse("Has ocurred an error updating the Course" + e.Message);
            }
        }

        public async Task<CourseResponse> UpdateAsync(int careerId, int id, Course course)
        {
            var existingCourse = await _courseRepository.FindById(id);
            if (existingCourse == null)
                return new CourseResponse("Course not found");
            if (existingCourse.CareerId != careerId)
                return new CourseResponse("Career not found for this course");

            try
            {
                existingCourse.Name = course.Name;
                
                _courseRepository.Update(existingCourse);
                await _unitOfWork.CompleteAsync();
                return new CourseResponse(existingCourse);
            }
            catch (Exception e)
            {
                return new CourseResponse("Has ocurred an error updating the Course" + e.Message);
            }
        }
    }
}
