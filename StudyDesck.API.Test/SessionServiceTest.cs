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
            var service = new SessionService(mockSessionRepository.Object, mockUnitOfWork.Object);

            // Act
            List<Session> result = (List<Session>)await service.ListAsync();
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
            var service = new SessionService(mockSessionRepository.Object, mockUnitOfWork.Object);

            // Act
            SessionResponse result = await service.GetByIdAsync(SessionId);
            var message = result.Message;

            // Assert
            message.Should().Be("Session not found");
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