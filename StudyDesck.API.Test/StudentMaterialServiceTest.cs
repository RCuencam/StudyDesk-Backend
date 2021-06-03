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
    class StudentMaterialServiceTest
    {
        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public async Task AssignStudentMaterialAsyncWhenStudentIdAndStudyMaterialIdIsValidReturnsStudentMaterialUpdate()
        {
            // Arrange
            var mockStudentMaterial = new Mock<IStudentMaterialRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStudyMaterial = new Mock<IStudyMaterialRepository>();
            int studentId = 1;
            int materialId = 1;
            int categoryId = 1;
            int instituteId = 1;
            var studentMaterial = new StudentMaterial()
            {
                StudentId = studentId,
                StudyMaterialId = materialId,
                CategoryId = categoryId,
                InstituteId = instituteId
            };
            mockStudentMaterial.Setup(r => r.AssignStudentMaterial(studentId, materialId, categoryId, instituteId));
            mockStudentMaterial.Setup(r => r.FindByStudentIdAndStudyMaterialId(studentId, materialId))
                .Returns(Task.FromResult(studentMaterial));
            var service = new StudentMaterialService(mockStudentMaterial.Object, mockUnitOfWork.Object, mockStudyMaterial.Object);

            // Act
            StudentMaterialResponse result = 
                await service.AssignStudentMaterialAsync(studentId, materialId);

            // Assert
            result.Should().Equals(studentMaterial);
        }

        [Test]
        public async Task AssignStudentMaterialAsyncWhenStudentIdAndStudyMaterialIdIsInvalidReturnsNull()
        {
            // Arrange
            var mockStudentMaterial = new Mock<IStudentMaterialRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStudyMaterial = new Mock<IStudyMaterialRepository>();
            int studentId = 1;
            int materialId = 1;
            int categoryId = 1;
            int instituteId = 1;
            mockStudentMaterial.Setup(r => r.FindByStudentIdAndStudyMaterialId(studentId, materialId))
                .Returns(Task.FromResult<StudentMaterial>(null));
            mockStudentMaterial.Setup(r => r.AssignStudentMaterial(studentId, materialId, categoryId, instituteId));
            var service = new StudentMaterialService(mockStudentMaterial.Object, mockUnitOfWork.Object, mockStudyMaterial.Object);

            // Act
            StudentMaterialResponse result =
                await service.AssignStudentMaterialAsync(studentId, materialId);

            // Assert
            result.Resource.Should().Equals(null);
        }


        [Test]
        public async Task UnassignStudentMaterialAsyncWhenStudentIdAndStudyMaterialIdIsValidReturnsStudentMaterial()
        {
            // Arrange
            var mockStudentMaterial = new Mock<IStudentMaterialRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStudyMaterial = new Mock<IStudyMaterialRepository>();

            int studentId = 1;
            int materialId = 1;
            int categoryId = 1;
            int instituteId = 1;
            var studentMaterial = new StudentMaterial()
            {
                StudentId = studentId,
                StudyMaterialId = materialId,
                CategoryId = categoryId,
                InstituteId = instituteId
            };
            mockStudentMaterial.Setup(r => r.UnassignstudyMaterial(studentId, materialId));
            mockStudentMaterial.Setup(r => r.FindByStudentIdAndStudyMaterialId(studentId, materialId))
                .Returns(Task.FromResult(studentMaterial));
            mockStudentMaterial.Setup(r => r.Remove(studentMaterial));
            var service = new StudentMaterialService(mockStudentMaterial.Object, mockUnitOfWork.Object, mockStudyMaterial.Object);

            // Act
            StudentMaterialResponse result =
                await service.UnassignStudentMaterialAsync(studentId, materialId);

            // Assert
            result.Should().Equals(studentMaterial);
        }

        [Test]
        public async Task UnassignStudentMaterialAsyncWhenStudentIdAndStudyMaterialIdIsInvalidReturnsNull()
        {
            // Arrange
            var mockStudentMaterial = new Mock<IStudentMaterialRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockStudyMaterial = new Mock<IStudyMaterialRepository>();

            int studentId = 1;
            int materialId = 1;
            mockStudentMaterial.Setup(r => r.FindByStudentIdAndStudyMaterialId(studentId, materialId))
                .Returns(Task.FromResult<StudentMaterial>(null));
            mockStudentMaterial.Setup(r => r.UnassignstudyMaterial(studentId, materialId));
            var service = new StudentMaterialService(mockStudentMaterial.Object, mockUnitOfWork.Object, mockStudyMaterial.Object);

            // Act
            StudentMaterialResponse result =
                await service.UnassignStudentMaterialAsync(studentId, materialId);

            // Assert
            result.Resource.Should().Equals(null);
        }

    }
}
