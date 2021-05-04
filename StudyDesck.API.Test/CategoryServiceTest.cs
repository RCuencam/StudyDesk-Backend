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
    class CategoryServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllAsyncWhenNoCategoriesReturnsEmptyCollection()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCategoryRepository = GetDefaultCategoryRepositoryInstance();

            mockCategoryRepository.Setup(r => r.ListAsync()).ReturnsAsync(new List<Category>());
            var service = new CategoryService(mockCategoryRepository.Object, mockUnitOfWork.Object);

            // Act
            List<Category> result = (List<Category>)await service.ListAsync();
            var categoryCount = result.Count;

            // Assert
            categoryCount.Should().Equals(0);
        }


        [Test]
        public async Task GetByIdAsyncWhenInvalidIdReturnsCategoryNotFoundResponse()
        {
            // Arrange
            var mockUnitOfWork = GetDefaultUnitOfWorkRepositoryInstance();
            var mockCategoryRepository = GetDefaultCategoryRepositoryInstance();
            var CategoryId = 1;

            mockCategoryRepository.Setup(r => r.FindById(CategoryId)).Returns(Task.FromResult<Category>(null));
            var service = new CategoryService(mockCategoryRepository.Object, mockUnitOfWork.Object);

            // Act
            CategoryResponse result = await service.GetByIdAsync(CategoryId);
            var message = result.Message;

            // Assert
            message.Should().Be("Category Not Found");
        }

        private Mock<ICategoryRepository> GetDefaultCategoryRepositoryInstance()
        {
            return new Mock<ICategoryRepository>();
        }
        private Mock<IUnitOfWork> GetDefaultUnitOfWorkRepositoryInstance()
        {
            return new Mock<IUnitOfWork>();
        }
    }
}
