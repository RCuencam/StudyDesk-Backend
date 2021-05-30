using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services.Comunications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> ListAsync();
        Task<CourseResponse> GetByIdAsync(int id);
        Task<CourseResponse> GetByIdAsync(int careerId, int id);
        Task<CourseResponse> SaveAsync(Course course);
        Task<CourseResponse> SaveAsync(int careerId, Course course);
        Task<CourseResponse> UpdateAsync(int id, Course course);
        Task<CourseResponse> UpdateAsync(int careerId, int id, Course course);
        Task<CourseResponse> DeleteAsync(int careerId, int id);
        Task<CourseResponse> DeleteAsync(int id);
        Task<IEnumerable<Course>> ListByCareerIdAsync(int careerId);
    }
}
