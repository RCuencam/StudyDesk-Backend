using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using FluentAssertions;
using StudyDesck.API.Domain.Persistence.Repositories;
using StudyDesck.API.Services;
using StudyDesck.API.Domain.Services.Comunications;
using StudyDesck.API.Domain.Models;

namespace StudyDesck.API.Test
{
    public class ScheduleServiceTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

       [Test]
        public async Task GetAllAsyncWhenNoSheduleReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSheduleRepository = GetDefaultSheduleRepositoryInstance();

            mockSheduleRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Shedule>());
            var service = new SheduleService(mockSheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            List<Shedule> result = (List<Shedule>)await service.ListAsync();
            var SheduleCount = result.Count;

            // Assert
            SheduleCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsSheduleNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSheduleRepository = GetDefaultSheduleRepositoryInstance();
            var SheduleId = 1;

            mockSheduleRepository.Setup(r => r.FindById(SheduleId)).Returns(Task.FromResult<Shedule>(null));
            var service = new SheduleService(mockSheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            SheduleResponse result = await service.GetByIdAsync(SheduleId);
            var message = result.Message;

            // Assert
            message.Should().Be("Shedule not found");
        }

        [Test]
        public async Task GetByIdAsyncWheIdIsCorrectReturnsSheduleInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSheduleRepository = GetDefaultSheduleRepositoryInstance();
            var SheduleId = 1;
            var Shedule = new Shedule()
            {
                Id = 1,
                StarDate = "string",
                EndDate = "string",
                Date = "string",
                TutorId = 1
            };

            mockSheduleRepository.Setup(r => r.FindById(SheduleId)).Returns(Task.FromResult(Shedule));
            var service = new SheduleService(mockSheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            SheduleResponse result = await service.GetByIdAsync(SheduleId);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(Shedule);
        }

        [Test]
        public async Task UpdateAsyncWhenIdIsCorrectReturnsSheduleInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSheduleRepository = GetDefaultSheduleRepositoryInstance();
            var SheduleId = 1;
            var Shedule = new Shedule()
            {
                Id = 1,
                StarDate = "string",
                EndDate = "string",
                Date = "string",
                TutorId = 1
            };

            mockSheduleRepository.Setup(r => r.FindById(SheduleId)).Returns(Task.FromResult(Shedule));
            mockSheduleRepository.Setup(r => r.Update(Shedule));
            var service = new SheduleService(mockSheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            SheduleResponse result = await service.UpdateAsync(SheduleId, Shedule);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(Shedule);
        }

        [Test]
        public async Task UpdateAsyncWhenInvalidIdReturnsSheduleNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSheduleRepository = GetDefaultSheduleRepositoryInstance();
            var SheduleId = 1;
            var Shedule = new Shedule()
            {
                Id = 1,
                StarDate = "string",
                EndDate = "string",
                Date = "string",
                TutorId = 1
            };

            mockSheduleRepository.Setup(r => r.FindById(SheduleId)).Returns(Task.FromResult<Shedule>(null));
            mockSheduleRepository.Setup(r => r.Update(Shedule));
            var service = new SheduleService(mockSheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            SheduleResponse result = await service.UpdateAsync(SheduleId, Shedule);
            var message = result.Message;

            // Assert
            message.Should().Be("Shedule not found");
        }

        [Test]
        public async Task DeleteAsyncWhenInvalidIdReturnsSheduleNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSheduleRepository = GetDefaultSheduleRepositoryInstance();
            var SheduleId = 1;
            var Shedule = new Shedule()
            {
                Id = 1,
                StarDate = "string",
                EndDate = "string",
                Date = "string",
                TutorId = 1
            };

            mockSheduleRepository.Setup(r => r.FindById(SheduleId)).Returns(Task.FromResult<Shedule>(null));
            mockSheduleRepository.Setup(r => r.Remove(Shedule));
            var service = new SheduleService(mockSheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            SheduleResponse result = await service.DeleteAsync(SheduleId);
            var message = result.Message;

            // Assert
            message.Should().Be("Shedule not found");
        }

        [Test]
        public async Task DeleteAsyncWhenIdIsCorrectReturnsSheduleInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockSheduleRepository = GetDefaultSheduleRepositoryInstance();
            var SheduleId = 1;
            var Shedule = new Shedule()
            {
                Id = 1,
                StarDate = "string",
                EndDate = "string",
                Date = "string",
                TutorId = 1
            };

            mockSheduleRepository.Setup(r => r.FindById(SheduleId)).Returns(Task.FromResult(Shedule));
            mockSheduleRepository.Setup(r => r.Remove(Shedule));
            var service = new SheduleService(mockSheduleRepository.Object, mockUnitOfWork.Object);

            // Act
            SheduleResponse result = await service.DeleteAsync(SheduleId);
            var instance = result.Resource;

            // Assert
            instance.Should().Be(Shedule);
        }

        private Mock<ISheduleRepository> GetDefaultSheduleRepositoryInstance()
        {
            return new Mock<ISheduleRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}