using FluentAssertions;
using Moq;
using NUnit.Framework;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Domain.Services.Comunications;
using StudyDesck.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyDesck.API.Test
{
    class SessionReservationTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetByStudentIdAndSessionIdAsyncWhenInvalidIdsReturnsNotFoundResponse()
        {
            //Arrange
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            var mockSessionReservationRepository = GetDefaultISessionReservationRepositoryInstance();
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            int sessionId = 1;
            int studentId = 1;

            mockSessionReservationRepository.Setup(sr => sr.FindByStudentIdAndSessionId(studentId, sessionId))
                .Returns(Task.FromResult<SessionReservation>(null));
            var service = new SessionReservationService(mockSessionReservationRepository.Object,mockSessionRepository.Object,mockStudentRepository.Object, mockUnitOfWork.Object);


            // Act
            SessionReservationResponse result = await service.GetByStudentIdAndSessionId(studentId,sessionId);
            var message = result.Message;

            // Assert
            message.Should().Be("This session reservation is not found");

        }

        [Test]
        public async Task GetAllAsyncWhenNoSessionReservationsReturnsEmptyCollection()
        {
            //Arrange
            var mockSessionReservationRepository = GetDefaultISessionReservationRepositoryInstance();
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            mockSessionReservationRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<SessionReservation>());

            var service = new SessionReservationService(mockSessionReservationRepository.Object, mockSessionRepository.Object,mockStudentRepository.Object,mockUnitOfWork.Object);
            //Act
            List<SessionReservation> result = (List<SessionReservation>)await service.ListAsync();
            var sessionReservationsCount = result.Count;
            //Assert
            sessionReservationsCount.Should().Equals(0);
        }

        /*
        [Test]
        public async Task AsyncCreateSessionReservationWhenSessionReservationAlreadyExists()
        {
            //Arrange
            var mockSessionReservationRepository = GetDefaultISessionReservationRepositoryInstance();
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();

            var session = new Session
            {
                Id = 1,
                Description = string.Empty,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Logo = string.Empty,
                Price = 20,
                QuantityMembers = 5,
                Title = "Fisica 1",
                CategoryID = 1,
                TutorID = 1,
            };

            var student = new Student
            {
                Id = 1,
                Name = "Josias",
                LastName = "Olaya",
                Password = "213123",
                Email = string.Empty,
                Logo = string.Empty,
                CareerId=1,
            };

            var sessionReservation = new SessionReservation
            {
                SessionId = 1,
                StudentId = 1,
                Confirmed = false,
                Qualification = 2,
            };

            mockSessionRepository.Setup(r => r.AddAsync(session));
            mockStudentRepository.Setup(r => r.AddAsync(student));
            mockSessionReservationRepository.Setup(r => r.AddAsync(sessionReservation));
            var service = new SessionReservationService(mockSessionReservationRepository.Object, mockSessionRepository.Object, mockStudentRepository.Object, mockUnitOfWork.Object);
            //var serviceSession = new SessionService(mockSessionRepository.Object, mockSessionReservationRepository.Object,mockUnitOfWork.Object);
            //var serviceStudent = new StudentService(mockStudentRepository.Object, mockSessionReservationRepository.Object, mockUnitOfWork.Object);

            // Act
            
            SessionReservationResponse result = await service.AssignSessionReservationAsync(student.Id, session.Id, sessionReservation);
            
            var message = result.Message;

            // Assert
            message.Should().Be("This session reservation already exist");
        }*/


        [Test]
        public async Task AsyncCreateSessionReservationWhenSessionIdOrStudentIdNotFound()
        {
            //Arrange
            var mockSessionReservationRepository = GetDefaultISessionReservationRepositoryInstance();
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            int studentId = 0;
            int sessionId = 0;

            var service = new SessionReservationService(mockSessionReservationRepository.Object, mockSessionRepository.Object, mockStudentRepository.Object, mockUnitOfWork.Object);

            // Act
            SessionReservationResponse result = await service.AssignSessionReservationAsync(studentId, sessionId, new SessionReservation { Confirmed = false, Qualification = 0 });
            var message = result.Message;

            // Assert
            message.Should().Be("SessionId or StudentId not found");
        }

        [Test]
        public async Task AsyncUpdateWhenSessionReservationNotFound()
        {
            //Arrange
            var mockSessionReservationRepository = GetDefaultISessionReservationRepositoryInstance();
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            int studentId = 0;
            int sessionId = 0;

            var service = new SessionReservationService(mockSessionReservationRepository.Object, mockSessionRepository.Object, mockStudentRepository.Object, mockUnitOfWork.Object);

            // Act
            SessionReservationResponse result = await service.UpdateSessionReservationAsync(studentId, sessionId, new SessionReservation { Confirmed = false, Qualification = 0 });
            var message = result.Message;

            // Assert
            message.Should().Be("SessionReservation not found");
        }

        [Test]
        public async Task AsyncDeleteWhenSessionReservationNotFound()
        {
            //Arrange
            var mockSessionReservationRepository = GetDefaultISessionReservationRepositoryInstance();
            var mockSessionRepository = GetDefaultISessionRepositoryInstance();
            var mockStudentRepository = GetDefaultIStudentRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstance();
            int studentId = 0;
            int sessionId = 0;

            var service = new SessionReservationService(mockSessionReservationRepository.Object, mockSessionRepository.Object, mockStudentRepository.Object, mockUnitOfWork.Object);

            // Act
            SessionReservationResponse result = await service.UpdateSessionReservationAsync(studentId, sessionId, new SessionReservation { Confirmed = false, Qualification = 0 });
            var message = result.Message;

            // Assert
            message.Should().Be("SessionReservation not found");
        }

        private Mock<ISessionReservationRepository> GetDefaultISessionReservationRepositoryInstance()
        {
            return new Mock<ISessionReservationRepository>();
        }

        private Mock<IStudentRepository> GetDefaultIStudentRepositoryInstance()
        {
            return new Mock<IStudentRepository>();
        }

        private Mock<ISessionRepository> GetDefaultISessionRepositoryInstance()
        {
            return new Mock<ISessionRepository>();
        }


        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
