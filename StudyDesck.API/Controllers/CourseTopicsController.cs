using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Extentions;
using StudyDesck.API.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/courses/{courseId}/topics")]
    [Produces("application/json")]
    public class CourseTopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public CourseTopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "List Topics by courseId")]
        [ProducesResponseType(typeof(IEnumerable<TopicResource>), 200)]
        public async Task<IEnumerable<TopicResource>> GetAllByCourseIdAsync(int courseId)
        {
            var topics = await _topicService.ListByCourseIdAsync(courseId);
            var resorces = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicResource>>(topics);

            return resorces;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "get a Topic of a Course")]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int courseId, int id)
        {
            var result = await _topicService.GetByIdAsync(courseId, id);
            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);

            return Ok(topicResource);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "create a Topic for a Course")]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int courseId, [FromBody] SaveTopicResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var topic = _mapper.Map<SaveTopicResource, Topic>(resource);
            var result = await _topicService.SaveAsync(courseId, topic);

            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);
            return Ok(topicResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "update a Topic of a Course")]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int courseId, int id, [FromBody] SaveTopicResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var topic = _mapper.Map<SaveTopicResource, Topic>(resource);
            var result = await _topicService.UpdateAsync(courseId, id, topic);

            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);
            return Ok(topicResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "delete a Topic of a Course")]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int courseId, int id)
        {
            var result = await _topicService.DeleteAsync(courseId, id);
            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);
            return Ok(topicResource);
        }
    }
}
