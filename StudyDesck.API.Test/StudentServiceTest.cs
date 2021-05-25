using NUnit.Framework;
using Moq;
using FluentAssertions;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using System.Threading.Tasks;
using System.Collections.Generic;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Services;


namespace StudyDesck.API.Test
{
    public class StudentServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoStudentsReturnsEmptyCollection()
        {
            //Arrange
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstace();

            mockStudentRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Student>());

            var service = new StudentService(mockStudentRepository.Object, null,mockUnitOfWork.Object);

            //Act
            List<Student> result = (List<Student>)await service.ListAsync();
            var studentsCount = result.Count;

            //Assert
            studentsCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsStudentsNotFoundResponse()
        {
            //Arrange
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstace();
            var studentId = 1;
            Student student = new Student();
            mockStudentRepository.Setup(r => r.FindById(studentId)).Returns(Task.FromResult<Student>(null));

            var service = new StudentService(mockStudentRepository.Object, null,mockUnitOfWork.Object);
            //Act
            StudentResponse result = await service.GetByIdAsync(studentId);
            var message = result.Message;
            //Assert
            message.Should().Be("Student not found");
        }

        private Mock<IStudentRepository> GetDefaultIStudentRepositoryInstance()
        {
            return new Mock<IStudentRepository>();
        }


        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstace()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
