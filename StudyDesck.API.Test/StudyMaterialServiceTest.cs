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
    class StudyMaterialServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoStudyMaterialsReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockStudyMaterialRepository = GetDefaultStudyMaterialRepositoryInstance();

            mockStudyMaterialRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<StudyMaterial>());
            var service = new StudyMaterialService(mockStudyMaterialRepository.Object, mockUnitOfWork.Object);

            // Act
            List<StudyMaterial> result = (List<StudyMaterial>)await service.ListAsync();
            var StudyMaterialCount = result.Count;

            // Assert
            StudyMaterialCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsStudyMaterialNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockStudyMaterialRepository = GetDefaultStudyMaterialRepositoryInstance();
            var StudyMaterialId = 1;

            mockStudyMaterialRepository.Setup(r => r.FindById(StudyMaterialId)).Returns(Task.FromResult<StudyMaterial>(null));
            var service = new StudyMaterialService(mockStudyMaterialRepository.Object, mockUnitOfWork.Object);

            // Act
            StudyMaterialResponse result = await service.GetByIdAsync(StudyMaterialId);
            var message = result.Message;

            // Assert
            message.Should().Be("Study material not found");
        }

        [Test]
        public async Task GetByIdAsyncWheIdIsCorrectReturnsStudyMaterialInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockStudyMaterialRepository = GetDefaultStudyMaterialRepositoryInstance();
            var StudyMaterialId = 1;
            var StudyMaterial = new StudyMaterial()
            {
                Id = 1,
                Title = "string",
                Description = "string",
                TopicId = 1
            };

            mockStudyMaterialRepository.Setup(r => r.FindById(StudyMaterialId)).Returns(Task.FromResult(StudyMaterial));
            var service = new StudyMaterialService(mockStudyMaterialRepository.Object, mockUnitOfWork.Object);

            // Act
            StudyMaterialResponse result = await service.GetByIdAsync(StudyMaterialId);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(StudyMaterial);
        }

        [Test]
        public async Task UpdateAsyncWhenIdIsCorrectReturnsStudyMaterialInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockStudyMaterialRepository = GetDefaultStudyMaterialRepositoryInstance();
            var StudyMaterialId = 1;
            var studyMaterial = new StudyMaterial()
            {
                Id = 1,
                Title = "string",
                Description = "string",
                TopicId = 1
            };

            mockStudyMaterialRepository.Setup(r => r.FindById(StudyMaterialId)).Returns(Task.FromResult(studyMaterial));
            mockStudyMaterialRepository.Setup(r => r.Update(studyMaterial));
            var service = new StudyMaterialService(mockStudyMaterialRepository.Object, mockUnitOfWork.Object);

            // Act
            StudyMaterialResponse result = await service.UpdateAsync(StudyMaterialId, studyMaterial);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(studyMaterial);
        }

        [Test]
        public async Task UpdateAsyncWhenInvalidIdReturnsStudyMaterialNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockStudyMaterialRepository = GetDefaultStudyMaterialRepositoryInstance();
            var StudyMaterialId = 1;
            var studyMaterial = new StudyMaterial()
            {
                Id = 1,
                Title = "string",
                Description = "string",
                TopicId = 1
            };

            mockStudyMaterialRepository.Setup(r => r.FindById(StudyMaterialId)).Returns(Task.FromResult<StudyMaterial>(null));
            mockStudyMaterialRepository.Setup(r => r.Update(studyMaterial));
            var service = new StudyMaterialService(mockStudyMaterialRepository.Object, mockUnitOfWork.Object);

            // Act
            StudyMaterialResponse result = await service.UpdateAsync(StudyMaterialId, studyMaterial);
            var message = result.Message;

            // Assert
            message.Should().Be("Study material not found");
        }

        [Test]
        public async Task DeleteAsyncWhenInvalidIdReturnsStudyMaterialNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockStudyMaterialRepository = GetDefaultStudyMaterialRepositoryInstance();
            var StudyMaterialId = 1;
            var studyMaterial = new StudyMaterial()
            {
                Id = 1,
                Title = "string",
                Description = "string",
                TopicId = 1
            };

            mockStudyMaterialRepository.Setup(r => r.FindById(StudyMaterialId)).Returns(Task.FromResult<StudyMaterial>(null));
            mockStudyMaterialRepository.Setup(r => r.Remove(studyMaterial));
            var service = new StudyMaterialService(mockStudyMaterialRepository.Object, mockUnitOfWork.Object);

            // Act
            StudyMaterialResponse result = await service.DeleteAsync(StudyMaterialId);
            var message = result.Message;

            // Assert
            message.Should().Be("Study material not found");
        }

        [Test]
        public async Task DeleteAsyncWhenIdIsCorrectReturnsStudyMaterialInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockStudyMaterialRepository = GetDefaultStudyMaterialRepositoryInstance();
            var StudyMaterialId = 1;
            var StudyMaterial = new StudyMaterial()
            {
                Id = 1,
                Title = "string",
                Description = "string",
                TopicId = 1
            };

            mockStudyMaterialRepository.Setup(r => r.FindById(StudyMaterialId)).Returns(Task.FromResult(StudyMaterial));
            mockStudyMaterialRepository.Setup(r => r.Remove(StudyMaterial));
            var service = new StudyMaterialService(mockStudyMaterialRepository.Object, mockUnitOfWork.Object);

            // Act
            StudyMaterialResponse result = await service.DeleteAsync(StudyMaterialId);
            var instance = result.Resource;

            // Assert
            instance.Should().Be(StudyMaterial);
        }

        private Mock<IStudyMaterialRepository> GetDefaultStudyMaterialRepositoryInstance()
        {
            return new Mock<IStudyMaterialRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
