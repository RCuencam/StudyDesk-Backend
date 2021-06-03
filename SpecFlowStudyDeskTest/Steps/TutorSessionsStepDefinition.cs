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
using TechTalk.SpecFlow;

namespace SpecFlowStudyDeskTest.Steps
{
    [Binding]
    public class TutorSessionsStepDefinition
    {
        private readonly ScenarioContext _scenarioContext;

        private TutorSessionsController _controller;
        private Session sessionTemp;
        private SaveSessionResource saveSessionTemp;
        private Tutor tutorTemp;

        private Mock<ISessionService> mockSessionService;
        private Mock<IMapper> mockMapper;
        private OkObjectResult _okResult;
        private BadRequestObjectResult _badResult;

        public TutorSessionsStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;              
        }

        [Given(@"a tutor named ""(.*)"" with a TutorId ""(.*)""")]
        public void GivenATutorNamedWithATutorId(string p0, int p1)
        {
            tutorTemp = new Tutor { Id = p1, Name = p0 };
        }


        [Given(@"a session named ""(.*)""")]
        public void GivenASessionNamed(string p0)
        {
            sessionTemp = new Session { Id = 1, Title = p0,Price=35 };
            saveSessionTemp = new SaveSessionResource { Title = p0, Price = 35 };
        }


        /*
        [When(@"I try to schedule a session with invalid TutorId such as (.*)")]
        public async void WhenITryToScheduleASessionWithInvalidTutorIdSuchAs(int p0)
        {
            var mockSessionService = GetDefaultSessionServiceInstance();
            var mockMapper = GetDefaultMapperInstance();
            Session session = new Session {Id=1, Title = "Taller 1", Price = 35 };
            SaveSessionResource saveSession = new SaveSessionResource {Title = "Taller 1", Price = 35 };

            mockMapper.Setup(r => r.Map<SaveSessionResource, Session>(saveSession)).Returns(session);

            mockSessionService.Setup(r => r.SaveAsync(p0, session)).ReturnsAsync(new SessionResponse("Id de tutor no encontrado"));
            _controller = new TutorSessionsController(mockSessionService.Object, mockMapper.Object);


            
            _actionResult = (BadRequestObjectResult)await _controller.PostAsync(p0, saveSession);
            
        }*/

        //SCENARIO 1
        [Given(@"I am in the tutorial session section \(one\)")]
        public void GivenIAmInTheTutorialSessionSectionOne()
        {
            //Arrange
            mockMapper = GetDefaultMapperInstance();
            mockSessionService = GetDefaultSessionServiceInstance();

            SessionResponse var1 = new SessionResponse(sessionTemp);
            mockMapper.Setup(r => r.Map<SaveSessionResource, Session>(saveSessionTemp)).Returns(sessionTemp);
            mockSessionService.Setup(r => r.SaveAsync(tutorTemp.Id, sessionTemp)).ReturnsAsync(var1);//SAVE
            mockMapper.Setup(r => r.Map<Session, SessionResource>(var1.Resource)).Returns(new SessionResource { Id = sessionTemp.Id, Title = sessionTemp.Title, Price = sessionTemp.Price });
        }


        [When(@"I try to schedule a session")]
        public async void WhenITryToScheduleASession()
        {
            //Act
            _controller = new TutorSessionsController(mockSessionService.Object, mockMapper.Object);
            _okResult = (OkObjectResult)await _controller.PostAsync(tutorTemp.Id, saveSessionTemp);
        }

        [Then(@"I should see ""(.*)""")]
        public void ThenIShouldSee(string p0)
        {
            //Assert
            string message = "";
            if (
                (_okResult != null && _okResult.StatusCode == 200) ||
                (_badResult != null && _badResult.StatusCode == 400))
                message = p0;

            message.Should().Be(p0);
        }

        //SCENARIO 2
        [Given(@"I am in the tutorial session section \(two\)")]
        public void GivenIAmInTheTutorialSessionSectionTwo()
        {
            //Arrange
            mockMapper = GetDefaultMapperInstance();
            mockSessionService = GetDefaultSessionServiceInstance();

            SessionResponse var1 = new SessionResponse(sessionTemp);
            mockMapper.Setup(r => r.Map<SaveSessionResource, Session>(saveSessionTemp)).Returns(sessionTemp);
            mockSessionService.Setup(r => r.SaveAsync(tutorTemp.Id, sessionTemp)).ReturnsAsync(var1);//SAVE
            mockSessionService.Setup(r => r.DeleteAsync(tutorTemp.Id, sessionTemp.Id)).ReturnsAsync(var1);//DELETE
            mockMapper.Setup(r => r.Map<Session, SessionResource>(var1.Resource)).Returns(new SessionResource { Id = sessionTemp.Id, Title = sessionTemp.Title, Price = sessionTemp.Price });
        }

        [When(@"I try to schedule a session and I get confused in a data")]
        public async void WhenITryToScheduleASessionAndIGetConfusedInAData()
        {
            //Act
            _controller = new TutorSessionsController(mockSessionService.Object, mockMapper.Object);
            _okResult = (OkObjectResult)await _controller.PostAsync(tutorTemp.Id, saveSessionTemp);
        }

        [Then(@"I should can deleting this session")]
        public async void ThenIShouldCanDeletingThisSession()
        {
            var _okResult2 = (OkObjectResult)await _controller.DeleteAsync(tutorTemp.Id, sessionTemp.Id);

            //Assert
            _okResult.StatusCode.Should().Be(200);
            _okResult2.StatusCode.Should().Be(200);
        }


        //SCENARIO 3
        [Given(@"I am in the tutorial session section \(three\)")]
        public void GivenIAmInTheTutorialSessionSectionThree()
        {
            //Arrange
            mockMapper = GetDefaultMapperInstance();
            mockSessionService = GetDefaultSessionServiceInstance();
            mockMapper.Setup(r => r.Map<SaveSessionResource, Session>(saveSessionTemp)).Returns(sessionTemp);
            mockSessionService.Setup(r => r.SaveAsync(tutorTemp.Id, sessionTemp)).ReturnsAsync(new SessionResponse("They cannot exist crossings of schedules"));
            
        }

        [When(@"I try to schedule a session that intersects at some time with another session that has already been scheduled")]
        public async void WhenITryToScheduleASessionThatIntersectsAtSomeTimeWithAnotherSessionThatHasAlreadyBeenScheduled()
        {
            //Act
            _controller = new TutorSessionsController(mockSessionService.Object, mockMapper.Object);
            _badResult = (BadRequestObjectResult)await _controller.PostAsync(tutorTemp.Id, saveSessionTemp);
        }

        private Mock<ISessionService> GetDefaultSessionServiceInstance()
        {
            return new Mock<ISessionService>();
        }

        private Mock<IMapper> GetDefaultMapperInstance()
        {
            return new Mock<IMapper>();
        }
    }
}
