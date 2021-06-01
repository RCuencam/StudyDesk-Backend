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
    class TutorServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }
        
        [Test]
        public async Task GetAllAsyncWhenNoTutorsReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTutorRepository = GetDefaultTutorRepositoryInstance();

            mockTutorRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Tutor>());
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            List<Tutor> result = (List<Tutor>)await service.ListAsync();
            var TutorCount = result.Count;

            // Assert
            TutorCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsTutorNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTutorRepository = GetDefaultTutorRepositoryInstance();
            var TutorId = 1;

            mockTutorRepository.Setup(r => r.FindById(TutorId)).Returns(Task.FromResult<Tutor>(null));
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.GetByIdAsync(TutorId);
            var message = result.Message;

            // Assert
            message.Should().Be("Tutor not found");
        }

        [Test]
        public async Task GetByIdAsyncWheIdIsCorrectReturnsTutorInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTutorRepository = GetDefaultTutorRepositoryInstance();
            var TutorId = 1;
            var Tutor = new Tutor()
            {
                Id = 1,
                Name = "string",
                LastName = "string",
                Description = "String",
                Logo = "string",
                Email = "string",
                Password = "string",
                PricePerHour = 0            };

            mockTutorRepository.Setup(r => r.FindById(TutorId)).Returns(Task.FromResult(Tutor));
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.GetByIdAsync(TutorId);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(Tutor);
        }

        [Test]
        public async Task UpdateAsyncWhenIdIsCorrectReturnsTutorInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTutorRepository = GetDefaultTutorRepositoryInstance();
            var TutorId = 1;
            var Tutor = new Tutor()
            {
                Id = 1,
                Name = "string",
                LastName = "string",
                Description = "String",
                Logo = "string",
                Email = "string",
                Password = "string",
                PricePerHour = 0
            };

            mockTutorRepository.Setup(r => r.FindById(TutorId)).Returns(Task.FromResult(Tutor));
            mockTutorRepository.Setup(r => r.Update(Tutor));
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.UpdateAsync(TutorId, Tutor);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(Tutor);
        }

        [Test]
        public async Task UpdateAsyncWhenInvalidIdReturnsTutorNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTutorRepository = GetDefaultTutorRepositoryInstance();
            var TutorId = 1;
            var Tutor = new Tutor()
            {
                Id = 1,
                Name = "string",
                LastName = "string",
                Description = "String",
                Logo = "string",
                Email = "string",
                Password = "string",
                PricePerHour = 0
            };

            mockTutorRepository.Setup(r => r.FindById(TutorId)).Returns(Task.FromResult<Tutor>(null));
            mockTutorRepository.Setup(r => r.Update(Tutor));
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.UpdateAsync(TutorId, Tutor);
            var message = result.Message;

            // Assert
            message.Should().Be("Tutor not found");
        }

        [Test]
        public async Task DeleteAsyncWhenInvalidIdReturnsTutorNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTutorRepository = GetDefaultTutorRepositoryInstance();
            var TutorId = 1;
            var Tutor = new Tutor()
            {
                Id = 1,
                Name = "string",
                LastName = "string",
                Description = "String",
                Logo = "string",
                Email = "string",
                Password = "string",
                PricePerHour = 0
            };

            mockTutorRepository.Setup(r => r.FindById(TutorId)).Returns(Task.FromResult<Tutor>(null));
            mockTutorRepository.Setup(r => r.Remove(Tutor));
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.DeleteAsync(TutorId);
            var message = result.Message;

            // Assert
            message.Should().Be("Tutor not found");
        }

        [Test]
        public async Task DeleteAsyncWhenIdIsCorrectReturnsTutorInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTutorRepository = GetDefaultTutorRepositoryInstance();
            var TutorId = 1;
            var Tutor = new Tutor()
            {
                Id = 1,
                Name = "string",
                LastName = "string",
                Description = "String",
                Logo = "string",
                Email = "string",
                Password = "string",
                PricePerHour = 0
            };

            mockTutorRepository.Setup(r => r.FindById(TutorId)).Returns(Task.FromResult(Tutor));
            mockTutorRepository.Setup(r => r.Remove(Tutor));
            var service = new TutorService(mockTutorRepository.Object, mockUnitOfWork.Object);

            // Act
            TutorResponse result = await service.DeleteAsync(TutorId);
            var instance = result.Resource;

            // Assert
            instance.Should().Be(Tutor);
        }
        
          private Mock<ITutorRepository> GetDefaultTutorRepositoryInstance()
        {
            return new Mock<ITutorRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
