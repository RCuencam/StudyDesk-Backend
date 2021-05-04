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
    public class CareerServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoCareersReturnsEmptyCollection()
        {
            //Arrange
            var mockCareerRepository = GetDefaultICareerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstace();

            mockCareerRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Career>());

            var service = new CareerService(mockCareerRepository.Object, mockUnitOfWork.Object);

            //Act
            List<Career> result = (List<Career>)await service.ListAsync();
            var careersCount = result.Count;

            //Assert
            careersCount.Should().Equals(0);
        }

        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCareersNotFoundResponse()
        {
            //Arrange
            var mockCareerRepository = GetDefaultICareerRepositoryInstance();
            var mockUnitOfWork = GetDefaultIUnitOfWorkInstace();
            var careerId = 1;
            Career career = new Career();
            mockCareerRepository.Setup(r => r.FindById(careerId)).Returns(Task.FromResult<Career>(null));

            var service = new CareerService(mockCareerRepository.Object, mockUnitOfWork.Object);
            //Act
            CareerResponse result = await service.GetByIdAsync(careerId);
            var message = result.Message;
            //Assert
            message.Should().Be("Career not found");
        }
        private Mock<ICareerRepository> GetDefaultICareerRepositoryInstance()
        {
            return new Mock<ICareerRepository>();
        }

        private Mock<IUnitOfWork> GetDefaultIUnitOfWorkInstace()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}

