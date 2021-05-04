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
    class PlatformServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoPlatformsReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockPlatformRepository = GetDefaultPlatformRepositoryInstance();

            mockPlatformRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Platform>());
            var service = new PlatformService(mockPlatformRepository.Object, mockUnitOfWork.Object);

            // Act
            List<Platform> result = (List<Platform>)await service.ListAsync();
            var platformCount = result.Count;

            // Assert
            platformCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsPlatformNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockPlatformRepository = GetDefaultPlatformRepositoryInstance();
            var PlatformId = 1;

            mockPlatformRepository.Setup(r => r.FindById(PlatformId)).Returns(Task.FromResult<Platform>(null));
            var service = new PlatformService(mockPlatformRepository.Object, mockUnitOfWork.Object);

            // Act
            PlatformResponse result = await service.GetByIdAsync(PlatformId);
            var message = result.Message;

            // Assert
            message.Should().Be("Platform not found");
        }

        private Mock<IPlatformRepository> GetDefaultPlatformRepositoryInstance()
        {
            return new Mock<IPlatformRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}