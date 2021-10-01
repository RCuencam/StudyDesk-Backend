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
    public class StudyMaterialsController : ControllerBase
    {
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly IMapper _mapper;
        public StudyMaterialsController(IStudyMaterialService studyMaterialService, IMapper mapper)
        {
            _studyMaterialService = studyMaterialService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all study materials")]
        [ProducesResponseType(typeof(IEnumerable<StudyMaterialResource>), 200)]
        public async Task<IEnumerable<StudyMaterialResource>> GetAllAsync()
        {
            var StudyMaterials = await _studyMaterialService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<StudyMaterial>, IEnumerable<StudyMaterialResource>>(StudyMaterials);

            return resorces;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "List a study material by its id")]
        [ProducesResponseType(typeof(StudyMaterialResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _studyMaterialService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var StudyMaterialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource);

            return Ok(StudyMaterialResource);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a study material")]
        [ProducesResponseType(typeof(StudyMaterialResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveStudyMaterialResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var StudyMaterial = _mapper.Map<SaveStudyMaterialResource, StudyMaterial>(resource);
            var result = await _studyMaterialService.SaveAsync(StudyMaterial);

            if (!result.Success)
                return BadRequest(result.Message);

            var StudyMaterialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource);
            return Ok(StudyMaterialResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a study material")]
        [ProducesResponseType(typeof(StudyMaterialResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveStudyMaterialResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var StudyMaterial = _mapper.Map<SaveStudyMaterialResource, StudyMaterial>(resource);
            var result = await _studyMaterialService.UpdateAsync(id, StudyMaterial);

            if (!result.Success)
                return BadRequest(result.Message);

            var StudyMaterialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource);
            return Ok(StudyMaterialResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a study material")]
        [ProducesResponseType(typeof(StudyMaterialResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _studyMaterialService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var StudyMaterialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource);
            return Ok(StudyMaterialResource);
        }
    }
    
}
