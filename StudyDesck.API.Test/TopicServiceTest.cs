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
    class TopicServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoTopicsReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTopicRepository = GetDefaultTopicRepositoryInstance();

            mockTopicRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Topic>());
            var service = new TopicService(mockTopicRepository.Object, mockUnitOfWork.Object);

            // Act
            List<Topic> result = (List<Topic>)await service.ListAsync();
            var topicCount = result.Count;

            // Assert
            topicCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsTopicNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTopicRepository = GetDefaultTopicRepositoryInstance();
            var TopicId = 1;

            mockTopicRepository.Setup(r => r.FindById(TopicId)).Returns(Task.FromResult<Topic>(null));
            var service = new TopicService(mockTopicRepository.Object, mockUnitOfWork.Object);

            // Act
            TopicResponse result = await service.GetByIdAsync(TopicId);
            var message = result.Message;

            // Assert
            message.Should().Be("Topic not found");
        }

        [Test]
        public async Task GetByIdAsyncWheIdIsCorrectReturnsTopicInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTopicRepository = GetDefaultTopicRepositoryInstance();
            var TopicId = 1;
            var topic = new Topic()
            {
                Id = 1,
                Name = "string",
                CourseId = 1
            };

            mockTopicRepository.Setup(r => r.FindById(TopicId)).Returns(Task.FromResult(topic));
            var service = new TopicService(mockTopicRepository.Object, mockUnitOfWork.Object);

            // Act
            TopicResponse result = await service.GetByIdAsync(TopicId);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(topic);
        }

        [Test]
        public async Task UpdateAsyncWhenIdIsCorrectReturnsTopicInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTopicRepository = GetDefaultTopicRepositoryInstance();
            var TopicId = 1;
            var topic = new Topic()
            {
                Id = 1,
                Name = "string",
                CourseId = 1
            };

            mockTopicRepository.Setup(r => r.FindById(TopicId)).Returns(Task.FromResult(topic));
            mockTopicRepository.Setup(r => r.Update(topic));
            var service = new TopicService(mockTopicRepository.Object, mockUnitOfWork.Object);

            // Act
            TopicResponse result = await service.UpdateAsync(TopicId, topic);
            var resource = result.Resource;

            // Assert
            resource.Should().Equals(topic);
        }

        [Test]
        public async Task UpdateAsyncWhenInvalidIdReturnsTopicNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTopicRepository = GetDefaultTopicRepositoryInstance();
            var TopicId = 1;
            var topic = new Topic()
            {
                Id = 1,
                Name = "string",
                CourseId = 1
            };

            mockTopicRepository.Setup(r => r.FindById(TopicId)).Returns(Task.FromResult<Topic>(null));
            mockTopicRepository.Setup(r => r.Update(topic));
            var service = new TopicService(mockTopicRepository.Object, mockUnitOfWork.Object);

            // Act
            TopicResponse result = await service.UpdateAsync(TopicId, topic);
            var message = result.Message;

            // Assert
            message.Should().Be("Topic not found");
        }

        [Test]
        public async Task DeleteAsyncWhenInvalidIdReturnsTopicNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTopicRepository = GetDefaultTopicRepositoryInstance();
            var TopicId = 1;
            var topic = new Topic()
            {
                Id = 1,
                Name = "string",
                CourseId = 1
            };

            mockTopicRepository.Setup(r => r.FindById(TopicId)).Returns(Task.FromResult<Topic>(null));
            mockTopicRepository.Setup(r => r.Remove(topic));
            var service = new TopicService(mockTopicRepository.Object, mockUnitOfWork.Object);

            // Act
            TopicResponse result = await service.DeleteAsync(TopicId);
            var message = result.Message;

            // Assert
            message.Should().Be("Topic not found");
        }

        [Test]
        public async Task DeleteAsyncWhenIdIsCorrectReturnsTopicInstance()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockTopicRepository = GetDefaultTopicRepositoryInstance();
            var TopicId = 1;
            var topic = new Topic()
            {
                Id = 1,
                Name = "string",
                CourseId = 1
            };

            mockTopicRepository.Setup(r => r.FindById(TopicId)).Returns(Task.FromResult(topic));
            mockTopicRepository.Setup(r => r.Remove(topic));
            var service = new TopicService(mockTopicRepository.Object, mockUnitOfWork.Object);

            // Act
            TopicResponse result = await service.DeleteAsync(TopicId);
            var instance = result.Resource;

            // Assert
            instance.Should().Be(topic);
        }

        private Mock<ITopicRepository> GetDefaultTopicRepositoryInstance()
        {
            return new Mock<ITopicRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
