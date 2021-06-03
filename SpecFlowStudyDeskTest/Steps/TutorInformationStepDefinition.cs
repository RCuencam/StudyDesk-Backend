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
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowStudyDeskTest.Steps
{
    [Binding]
    public sealed class TutorInformationStepDefinition
    {
        private CareerTutorsController _careertutorController;

        private OkObjectResult _okResult;
        private Mock<ITutorService> _tutorService;
        private Mock<IMapper> _mapper;

        private Tutor _tutor;
        private SaveTutorResource _saveTutor;
        private Career _career;
       
        private readonly ScenarioContext _scenarioContext;


        public TutorInformationStepDefinition(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _tutorService = new Mock<ITutorService>();
            _mapper = new Mock<IMapper>();

        }

        [Given(@"a tutor with name ""(.*)"" description ""(.*)"" and id (.*)")]
        public void GivenATutorWithNameDescriptionAndId(string name, string descripcion, int tutorId)
        {
            _tutor = new Tutor { Id = tutorId, Name = name, Description = descripcion };
            _career = new Career { Id = 1 };
            _saveTutor = new SaveTutorResource { Name = name, Description = descripcion};

            _mapper = GetDefaultMapperInstance();
            _tutorService = GetDefaultTutorServiceInstance();

            TutorResponse var1 = new TutorResponse(_tutor);
            _mapper.Setup(r => r.Map<SaveTutorResource, Tutor>(_saveTutor)).Returns(_tutor);
            _tutorService.Setup(r => r.GetByIdAsync (_tutor.Id)).ReturnsAsync(var1);
            _mapper.Setup(r => r.Map<Tutor, TutorResource>(var1.Resource)).Returns(new TutorResource { Id = _tutor.Id,CareerId = _career.Id, Name = _tutor.Name, Description = _tutor.Description});
        }

        [When(@"the student select a tutor with id (.*)")]
        public async void WhenTheStudentSelectATutorWithId(int tutorId)
        {
            _careertutorController = new CareerTutorsController(_tutorService.Object, _mapper.Object);
            _okResult = (OkObjectResult)await _careertutorController.GetAsync(_tutor.Id);
        }

        [Then(@"he get a tutor with name ""(.*)"" description ""(.*)"" and id (.*)")]
        public void ThenHeGetATutorWithNameDescriptionAndId(string name, string descripcion, int tutorId)
        {
            var tutor = (TutorResource)_okResult.Value;
            tutor.Id.CompareTo(tutorId);
            tutor.Name.Should().Be(name);
            tutor.Description.Should().Be(descripcion);
        }


        [Given(@"a tutor with name ""(.*)"" without description and id (.*)")]
        public void GivenATutorWithNameWithoutDescriptionAndId(string name, int tutorId)
        {
            _tutor = new Tutor { Id = tutorId, Name = name};
            _career = new Career { Id = 1 };
            _saveTutor = new SaveTutorResource { Name = name };

            _mapper = GetDefaultMapperInstance();
            _tutorService = GetDefaultTutorServiceInstance();

            TutorResponse var1 = new TutorResponse(_tutor);
            _mapper.Setup(r => r.Map<SaveTutorResource, Tutor>(_saveTutor)).Returns(_tutor);
            _tutorService.Setup(r => r.GetByIdAsync(_tutor.Id)).ReturnsAsync(var1);
            _mapper.Setup(r => r.Map<Tutor, TutorResource>(var1.Resource)).Returns(new TutorResource { Id = _tutor.Id, CareerId = _career.Id, Name = _tutor.Name});

        }

        [Then(@"he get a tutor with name ""(.*)"" without description and id (.*)")]
        public void ThenHeGetATutorWithNameWithoutDescriptionAndId(string name, int tutorId)
        {
            var tutor = (TutorResource)_okResult.Value;
            tutor.Id.CompareTo(tutorId);
            tutor.Name.Should().Be(name);
            tutor.Description.Should().Be(null);
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
