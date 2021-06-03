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
    public class SelectTopicTutorSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private CourseTopicsController _controller;
        private Mock<ITopicService> service;
        private Mock<IMapper> mapper;

        private OkObjectResult _okResult;
        private int result;
        private int id = 1;
        SaveTopicResource save;

        public SelectTopicTutorSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            
        }

        [Given(@"exists a list of topics with (.*) elemets")]
        public void GivenExistsAListOfTopicsWithElemets(int sizeTopics)
        {
            service = new Mock<ITopicService>();
            mapper = new Mock<IMapper>();
            // Arrange
            Topic topic = new Topic();
            List<Topic> topics = new List<Topic>();
            for (int i = 0; i < sizeTopics; i++)
                topics.Add(topic);
            service.Setup(r => r.ListByCourseIdAsync(id)).ReturnsAsync(topics);
            _controller = new CourseTopicsController(service.Object, mapper.Object);
        }

        [When(@"a tutor selects topics options")]
        public async void WhenATutorSelectsTopicsOptions()
        {

            // Action
            var topics = await _controller.GetAllByCourseIdAsync(id);
            result = topics.Count();
        }

        [Then(@"the result is a list of (.*) elements")]
        public void ThenTheResultIsAListOfElements(int listElements)
        {
            // Assert
            result.CompareTo(listElements);
        }


        [Given(@"the tutor cannot find the topic with name ""(.*)""")]
        public void GivenTheTutorCannotFindTheTopicWithName(string name)
        {
            // arrange
            service = new Mock<ITopicService>();
            mapper = new Mock<IMapper>();
            save = new SaveTopicResource();
            TopicResource topicResource = new TopicResource();
            topicResource.Name = name;
            save.Name = name;
            Topic topic = new Topic();
            topic.Name = name;

            mapper.Setup(r => r.Map<SaveTopicResource, Topic>(save)).Returns(topic);
            service.Setup(r => r.SaveAsync(id, topic)).ReturnsAsync(new TopicResponse(topic));
            mapper.Setup(r => r.Map<Topic, TopicResource>(topic)).Returns(topicResource);

            _controller = new CourseTopicsController(service.Object, mapper.Object);
        }

        [When(@"add new topic with the name ""(.*)""")]
        public async void WhenAddNewTopicWithTheName(string name)
        {
            // Action

            _okResult = (OkObjectResult)await _controller.PostAsync(id, save);
        }


        [Then(@"it is added the topic with the name ""(.*)"" to the list of topics")]
        public void ThenItIsAddedTheTopicWithTheNameToTheListOfTopics(string name)
        {
            // Assert
            var topic = (TopicResource)_okResult.Value;
            topic.Name.Should().Be(name);
        }
    }
}
