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
    [Route("/api/topics/{topicId}/materials")]
    [Produces("application/json")]
    public class TopicMaterialsController : ControllerBase
    {
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly IMapper _mapper;

        public TopicMaterialsController(IStudyMaterialService studyMaterialService, IMapper mapper)
        {
            _studyMaterialService = studyMaterialService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all material of a topic")]
        public async Task<IEnumerable<StudyMaterialResource>> GetAllByTopicIdAsync(int topicId)
        {
            var materials = await _studyMaterialService.ListByTopicIdAsync(topicId);
            var resources = _mapper.Map<IEnumerable<StudyMaterial>, IEnumerable<StudyMaterialResource>>(materials);
            return resources;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a study material")]
        [ProducesResponseType(typeof(StudyMaterialResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int topicId,[FromBody] SaveStudyMaterialResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var StudyMaterial = _mapper.Map<SaveStudyMaterialResource, StudyMaterial>(resource);
            var result = await _studyMaterialService.SaveAsync(topicId,StudyMaterial);

            if (!result.Success)
                return BadRequest(result.Message);

            var StudyMaterialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource);
            return Ok(StudyMaterialResource);
        }
    }
}
