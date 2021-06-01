using FluentAssertions;
using Moq;
using NUnit.Framework;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services.Comunications;
using StudyDesck.API.Persistence.Repositories;
using StudyDesck.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyDesck.API.Test
{
    class CourseServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoCoursesReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCourseRepository = GetDefaultCourseRepositoryInstance();
            var mockCareerRepository = new Mock<ICareerRepository>();

            mockCourseRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Course>());
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object, mockCareerRepository.Object);

            // Act
            List<Course> result = (List<Course>)await service.ListAsync();
            var courseCount = result.Count;

            // Assert
            courseCount.Should().Equals(0);
        }

        
        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCourseNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCourseRepository = GetDefaultCourseRepositoryInstance();
            var mockCareerRepository = new Mock<ICareerRepository>();

            var CourseId = 1;

            mockCourseRepository.Setup(r => r.FindById(CourseId)).Returns(Task.FromResult<Course>(null));
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object, mockCareerRepository.Object);

            // Act
            CourseResponse result = await service.GetByIdAsync(CourseId);
            var message = result.Message;

            // Assert
            message.Should().Be("course not found");
        }

        [Test]
        public async Task GetByIdAsyncWheIdIsCorrectReturnsCourseInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCourseRepository = GetDefaultCourseRepositoryInstance();
            var mockCareerRepository = new Mock<ICareerRepository>();

            var CourseId = 1;
            var course = new Course()
            {
                Id = 1,
                Name = "string",
                CareerId = 1
            };

            mockCourseRepository.Setup(r => r.FindById(CourseId)).Returns(Task.FromResult(course));
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object, mockCareerRepository.Object);

            // Act
            CourseResponse result = await service.GetByIdAsync(CourseId);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(course);
        }

        [Test]
        public async Task UpdateAsyncWhenIdIsCorrectReturnsCourseInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCourseRepository = GetDefaultCourseRepositoryInstance();
            var mockCareerRepository = new Mock<ICareerRepository>();

            var CourseId = 1;
            var course = new Course()
            {
                Id = 1,
                Name = "string",
                CareerId = 1
            };

            mockCourseRepository.Setup(r => r.FindById(CourseId)).Returns(Task.FromResult(course));
            mockCourseRepository.Setup(r => r.Update(course));
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object, mockCareerRepository.Object);

            // Act
            CourseResponse result = await service.UpdateAsync(CourseId, course);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(course);
        }

        [Test]
        public async Task UpdateAsyncWhenInvalidIdReturnsCourseNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCourseRepository = GetDefaultCourseRepositoryInstance();
            var mockCareerRepository = new Mock<ICareerRepository>();

            var CourseId = 1;
            var course = new Course()
            {
                Id = 1,
                Name = "string",
                CareerId = 1
            };

            mockCourseRepository.Setup(r => r.FindById(CourseId)).Returns(Task.FromResult<Course>(null));
            mockCourseRepository.Setup(r => r.Update(course));
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object, mockCareerRepository.Object);

            // Act
            CourseResponse result = await service.UpdateAsync(CourseId, course);
            var message = result.Message;

            // Assert
            message.Should().Be("Course not found");
        }

        [Test]
        public async Task DeleteAsyncWhenInvalidIdReturnsCourseNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCourseRepository = GetDefaultCourseRepositoryInstance();
            var mockCareerRepository = new Mock<ICareerRepository>();

            var CourseId = 1;
            var course = new Course()
            {
                Id = 1,
                Name = "string",
                CareerId = 1
            };

            mockCourseRepository.Setup(r => r.FindById(CourseId)).Returns(Task.FromResult<Course>(null));
            mockCourseRepository.Setup(r => r.Remove(course));
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object, mockCareerRepository.Object);

            // Act
            CourseResponse result = await service.DeleteAsync(CourseId);
            var message = result.Message;

            // Assert
            message.Should().Be("Course not found");
        }

        [Test]
        public async Task DeleteAsyncWhenIdIsCorrectReturnsCourseInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCourseRepository = GetDefaultCourseRepositoryInstance();
            var mockCareerRepository = new Mock<ICareerRepository>();

            var CourseId = 1;
            var course = new Course()
            {
                Id = 1,
                Name = "string",
                CareerId = 1
            };

            mockCourseRepository.Setup(r => r.FindById(CourseId)).Returns(Task.FromResult(course));
            mockCourseRepository.Setup(r => r.Remove(course));
            var service = new CourseService(mockCourseRepository.Object, mockUnitOfWork.Object, mockCareerRepository.Object);

            // Act
            CourseResponse result = await service.DeleteAsync(CourseId);
            var instance = result.Resource;

            // Assert
            instance.Should().Be(course);
        }

        private Mock<ICourseRepository> GetDefaultCourseRepositoryInstance()
        {
            return new Mock<ICourseRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }

    }
}
