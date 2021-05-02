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
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly IMapper _mapper;

        public TopicsController(ITopicService topicService, IMapper mapper)
        {
            _topicService = topicService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TopicResource>), 200)]
        public async Task<IEnumerable<TopicResource>> GetAllAsync()
        {
            var topics = await _topicService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<Topic>, IEnumerable<TopicResource>>(topics);

            return resorces;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _topicService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);

            return Ok(topicResource);
        }


        [HttpPost]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveTopicResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var topic = _mapper.Map<SaveTopicResource, Topic>(resource);
            var result = await _topicService.SaveAsync(topic);

            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);
            return Ok(topicResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTopicResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var topic = _mapper.Map<SaveTopicResource, Topic>(resource);
            var result = await _topicService.UpdateAsync(id, topic);

            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);
            return Ok(topicResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TopicResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _topicService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var topicResource = _mapper.Map<Topic, TopicResource>(result.Resource);
            return Ok(topicResource);
        }
    }
}
