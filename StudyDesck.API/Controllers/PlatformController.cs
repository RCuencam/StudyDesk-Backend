﻿using AutoMapper;
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
    [Produces("application/json")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformService platformService, IMapper mapper)
        {
            _platformService = platformService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlatformResource>), 200)]
        public async Task<IEnumerable<PlatformResource>> GetAllAsync()
        {
            var platforms = await _platformService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Platform>, IEnumerable<PlatformResource>>(platforms);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlatformResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _platformService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var PlatformResource = _mapper.Map<Platform, PlatformResource>(result.Resource);

            return Ok(PlatformResource);
        }


        [HttpPost]
        [ProducesResponseType(typeof(PlatformResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SavePlatformResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var platform = _mapper.Map<SavePlatformResource, Platform>(resource);
            var result = await _platformService.SaveAsync(platform);

            if (!result.Success)
                return BadRequest(result.Message);

            var PlatformResource = _mapper.Map<Platform, PlatformResource>(result.Resource);
            return Ok(PlatformResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PlatformResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePlatformResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var platform = _mapper.Map<SavePlatformResource, Platform>(resource);
            var result = await _platformService.UpdateAsync(id, platform);

            if (!result.Success)
                return BadRequest(result.Message);

            var PlatformResource = _mapper.Map<Platform, PlatformResource>(result.Resource);
            return Ok(PlatformResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PlatformResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _platformService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var PlatformResource = _mapper.Map<Platform, PlatformResource>(result.Resource);
            return Ok(PlatformResource);
        }
    }
}
