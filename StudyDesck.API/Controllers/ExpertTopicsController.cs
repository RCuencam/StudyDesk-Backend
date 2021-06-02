using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Extentions;
using StudyDesck.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
    [Route("/api/[controller]")]
    public class ExpertTopicsController : ControllerBase
    {
        private readonly ITutorService _tutorService;
        private readonly ITopicService _topicService;
        private readonly IExpertTopicService _expertTopicService;
        private readonly IMapper _mapper;

        public ExpertTopicsController(ITutorService tutorService, ITopicService topicService, IExpertTopicService expertTopicService, IMapper mapper)
        {
            _tutorService = tutorService;
            _expertTopicService = expertTopicService;
            _topicService = topicService;
            _mapper = mapper;
        }

        [HttpGet("topics/{topicId}/tutor")]
        public async Task<IEnumerable<TutorResource>> GetAllByTopicTdAsync(int topicId)
        {
            var tutors = await _tutorService.ListByTopicIdAsync(topicId);
            var resources = _mapper.Map<IEnumerable<Tutor>, IEnumerable<TutorResource>>(tutors);

            return resources;
        }

        [HttpGet("tutors/{tutorId}/topics")]
        public async Task<IEnumerable<TopicResource>> GetAllByTutorTdAsync(int tutorId)
        {
            var topics = await _topicService.ListByTutorIdAsync(tutorId);
            var resources = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicResource>>(topics);

            return resources;
        }

        [HttpPost("{tutorId}")]
        public async Task<IActionResult> AssignExpertTopic(int tutorId, int topicId)
        {
            var result = await _expertTopicService.AssignExpertTopicAsync(tutorId, topicId);

            if (!result.Success)
                return BadRequest(result.Message);

            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource.Tutor);

            return Ok(tutorResource);
        }

        //[HttpDelete]
        //public async Task<IActionResult> UnssingExpertTopic(int tutorId, int topicId)
        //{
        //    var result = await _expertTopicService.UnassignExpertTopicAsync(tutorId, topicId);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource.Tutor);

        //    return Ok(tutorResource);
        //}
    }
}
