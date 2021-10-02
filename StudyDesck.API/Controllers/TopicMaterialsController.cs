using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
    [ApiController]
    [Route("/api/topics/{topicId}/materials")]
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
    }
}
