using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StudyDesck.API.Controllers;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Domain.Services.Comunications;
using StudyDesck.API.Resources;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlowStudyDeskTest.Steps
{
    [Binding]
    class StudentReservationStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;

        private CareerTutorsController _controller;
        private Career carrerTemp;

        private Mock<ITutorService> mockTutorService;
        private Mock<IMapper> mockMapper;
        private IEnumerable<TutorResource> _okResult;

        public StudentReservationStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a career named ""(.*)"" with a Careerid ""(.*)""")]
        public void GivenACareerNamedWithACareerid(string p0, int p1)
        {
            carrerTemp = new Career { Id = p1, Name = p0 };
        }
        //Scenario 1
        [Given(@"I am in the finding section \(one\)")]
        public void GivenIAmInTheFindingSectionOne()
        {
            //Arrange
            mockMapper = GetDefaultMapperInstance();
            mockTutorService = GetDefaultTutorServiceInstance();

            List<Tutor> tutors = new List<Tutor>()
            {
                new Tutor{Id = 1, Name = "Omar", CareerId = carrerTemp.Id},
                new Tutor{Id = 2, Name = "Josias", CareerId = carrerTemp.Id}
            };
            IEnumerable<TutorResource> tutorsResource = new List<TutorResource>()
            {
                new TutorResource{Id = 1, Name = "Omar", CareerId = carrerTemp.Id},
                new TutorResource{Id = 2, Name = "Josias", CareerId = carrerTemp.Id}
            };

            mockTutorService.Setup(r => r.ListByCareerIdAsync(carrerTemp.Id)).ReturnsAsync(tutors);
            mockMapper.Setup(r => r.Map<IEnumerable<Tutor>, IEnumerable<TutorResource>>(tutors)).Returns(tutorsResource);
            
        }

        [When(@"I try to look for a tutorship \(one\)")]
        public async void WhenITryToLookForATutorship()
        {
            _controller = new CareerTutorsController(mockTutorService.Object, mockMapper.Object);
            _okResult = await _controller.GetAllByCareerIdAsync(carrerTemp.Id);
        }

        [Then(@"I should see a list of tutors")]
        public void ThenIShouldSeeAListOfTutors()
        {
            ((List<TutorResource>)_okResult).Count.Should().BeGreaterThan(0);
        }



        //SCENARIO 2
        [Given(@"I am in the finding section \(two\)")]
        public void GivenIAmInTheFindingSectionTwo()
        {
            //Arrange
            mockMapper = GetDefaultMapperInstance();
            mockTutorService = GetDefaultTutorServiceInstance();

            List<Tutor> tutors = new List<Tutor>()
            {
            };
            IEnumerable<TutorResource> tutorsResource = new List<TutorResource>()
            {
            };
            mockTutorService.Setup(r => r.ListByCareerIdAsync(carrerTemp.Id)).ReturnsAsync(tutors);
            mockMapper.Setup(r => r.Map<IEnumerable<Tutor>, IEnumerable<TutorResource>>(tutors)).Returns(tutorsResource);
        }

        [When(@"I try to look for a tutorship \(two\)")]
        public async void WhenITryToLookForATutorshipTwo()
        {
            _controller = new CareerTutorsController(mockTutorService.Object, mockMapper.Object);
            _okResult = await _controller.GetAllByCareerIdAsync(carrerTemp.Id);
        }

        [Then(@"I should SEE ""(.*)""")]
        public void ThenIShouldSEE(string p0)
        {
            string message = "";
            if (((List<TutorResource>)_okResult).Count == 0)
            {
                message = p0;
            }
            message.Should().Be(p0);
        }


        private Mock<ITutorService> GetDefaultTutorServiceInstance()
        {
            return new Mock<ITutorService>();
        }

        private Mock<IMapper> GetDefaultMapperInstance()
        {
            return new Mock<IMapper>();
        }

    }
}
