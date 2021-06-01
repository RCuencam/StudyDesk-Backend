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
    class SessionServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoSessionsReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();

            mockSessionRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Session>());
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);

            // Act
            List<Session> result = (List<Session>)await service.ListAsync();
            var sessionCount = result.Count;

            // Assert
            sessionCount.Should().Equals(0);
        }

        [Test]
        public async Task GetAllAsyncWhenSessionsReturnsACollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();

            var sessionsList = new List<Session>() {
                new Session { Id=1,Title="Repaso1"},
                new Session { Id=2,Title="Taller FIIE"},
                new Session { Id=3,Title="Practica 2"},
                new Session { Id=4,Title="Parcial Repaso"},
            };

            mockSessionRepository.Setup(r => r.ListAsync()).ReturnsAsync(sessionsList);
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);

            // Act
            List<Session> result = (List<Session>) await service.ListAsync();
            var sessionCount = result.Count;

            // Assert
            sessionCount.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task GetAllAsyncByCategoryIdWhenNoSessionsReturnsEmptyCollection()
        {
            // Arrange
            int categoryId = 1;
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();

            mockSessionRepository.Setup(r => r.ListByCategoryIdAsync(categoryId)).ReturnsAsync(new List<Session>());
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);

            // Act
            List<Session> result = (List<Session>)await service.ListByCategoryIdAsync(categoryId);
            var sessionCount = result.Count;

            // Assert
            sessionCount.Should().Equals(0);
        }

        [Test]
        public async Task GetAllAsyncByPlatformIdWhenNoSessionsReturnsEmptyCollection()
        {
            // Arrange
            int platformId = 1;
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();

            mockSessionRepository.Setup(r => r.ListByPlatformIdAsync(platformId)).ReturnsAsync(new List<Session>());
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);

            // Act
            List<Session> result = (List<Session>)await service.ListByPlatformIdAsync(platformId);
            var sessionCount = result.Count;

            // Assert
            sessionCount.Should().Equals(0);
        }

        [Test]
        public async Task GetAllAsyncByTutorIdWhenNoSessionsReturnsEmptyCollection()
        {
            // Arrange
            int tutorId = 1;
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();

            mockSessionRepository.Setup(r => r.ListByTutorIdAsync(tutorId)).ReturnsAsync(new List<Session>());
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);

            // Act
            List<Session> result = (List<Session>)await service.ListByTutorIdAsync(tutorId);
            var sessionCount = result.Count;

            // Assert
            sessionCount.Should().Equals(0);
        }

        [Test]
        public async Task GetAllAsyncByTopicIdWhenNoSessionsReturnsEmptyCollection()
        {
            // Arrange
            int topicId = 1;
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();

            mockSessionRepository.Setup(r => r.ListByTopicIdAsync(topicId)).ReturnsAsync(new List<Session>());
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);

            // Act
            List<Session> result = (List<Session>)await service.ListByTopicIdAsync(topicId);
            var sessionCount = result.Count;

            // Assert
            sessionCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsSessionNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();
            var SessionId = 1;

            mockSessionRepository.Setup(r => r.FindById(SessionId)).Returns(Task.FromResult<Session>(null));
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);

            // Act
            SessionResponse result = await service.GetByIdAsync(SessionId);
            var message = result.Message;

            // Assert
            message.Should().Be("Session not found");
        }

        [Test]
        public async Task GetByIdAsyncWhenValidIdReturnsExistingSession()
        {
            //arrage
            var mockSessionRepository = GetDefaultSessionRepositoryInstance();
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var sessionId = 1;
            var sessionDTO = new Session { Id = sessionId, Description = "Soy una sesion de examen" };
            
            mockSessionRepository.Setup(r => r.FindById(sessionId)).ReturnsAsync(sessionDTO);
            var service = new SessionService(mockSessionRepository.Object, null, null, mockUnitOfWork.Object);
            //act
            var session = await service.GetByIdAsync(sessionId);

            //assert
            Assert.AreEqual(sessionDTO, session.Resource);
        }





        private Mock<ISessionRepository> GetDefaultSessionRepositoryInstance()
        {
            return new Mock<ISessionRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}