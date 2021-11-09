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
    
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversityService _universityService;
        private readonly IMapper _mapper;

        public UniversitiesController(IUniversityService universityService, IMapper mapper)
        {
            _universityService = universityService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all universities")]
        [ProducesResponseType(typeof(IEnumerable<universityResource>), 200)]
        public async Task<IEnumerable<universityResource>> GetAllAsync()
        {
            var universitys = await _universityService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<University>, IEnumerable<universityResource>>(universitys);

            return resorces;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "List an university by id")]
        [ProducesResponseType(typeof(universityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _universityService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var universityResource = _mapper.Map<University, universityResource>(result.Resource);

            return Ok(universityResource);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create an university")]
        [ProducesResponseType(typeof(universityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveuniversityResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var university = _mapper.Map<SaveuniversityResource, University>(resource);
            var result = await _universityService.SaveAsync(university);

            if (!result.Success)
                return BadRequest(result.Message);

            var universityResource = _mapper.Map<University, universityResource>(result.Resource);
            return Ok(universityResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an university")]
        [ProducesResponseType(typeof(universityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveuniversityResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var university = _mapper.Map<SaveuniversityResource, University>(resource);
            var result = await _universityService.UpdateAsync(id, university);

            if (!result.Success)
                return BadRequest(result.Message);

            var universityResource = _mapper.Map<University, universityResource>(result.Resource);
            return Ok(universityResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an university")]
        [ProducesResponseType(typeof(universityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _universityService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var universityResource = _mapper.Map<University, universityResource>(result.Resource);
            return Ok(universityResource);
        }
    }
}

