﻿using AutoMapper;
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
    public class InstitutesController : ControllerBase
    {
        private readonly IInstituteService _instituteService;
        private readonly IMapper _mapper;

        public InstitutesController(IInstituteService instituteService, IMapper mapper)
        {
            _instituteService = instituteService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all institutes")]
        [ProducesResponseType(typeof(IEnumerable<InstituteResource>), 200)]
        public async Task<IEnumerable<InstituteResource>> GetAllAsync()
        {
            var institutes = await _instituteService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<Institute>, IEnumerable<InstituteResource>>(institutes);

            return resorces;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "List an institute by id")]
        [ProducesResponseType(typeof(InstituteResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _instituteService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var instituteResource = _mapper.Map<Institute, InstituteResource>(result.Resource);

            return Ok(instituteResource);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create an institute")]
        [ProducesResponseType(typeof(InstituteResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveInstituteResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var institute = _mapper.Map<SaveInstituteResource, Institute>(resource);
            var result = await _instituteService.SaveAsync(institute);

            if (!result.Success)
                return BadRequest(result.Message);

            var instituteResource = _mapper.Map<Institute, InstituteResource>(result.Resource);
            return Ok(instituteResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an institute")]
        [ProducesResponseType(typeof(InstituteResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveInstituteResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var institute = _mapper.Map<SaveInstituteResource, Institute>(resource);
            var result = await _instituteService.UpdateAsync(id, institute);

            if (!result.Success)
                return BadRequest(result.Message);

            var instituteResource = _mapper.Map<Institute, InstituteResource>(result.Resource);
            return Ok(instituteResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an institute")]
        [ProducesResponseType(typeof(InstituteResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _instituteService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var instituteResource = _mapper.Map<Institute, InstituteResource>(result.Resource);
            return Ok(instituteResource);
        }
    }
}

