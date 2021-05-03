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
    public class ShedulesController: ControllerBase
    {
        private readonly ISheduleService _sheduleService;
        private readonly IMapper _mapper;

        public ShedulesController(ISheduleService sheduleService, IMapper mapper)
        {
            _sheduleService = sheduleService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SheduleResource>), 200)]
        public async Task<IEnumerable<SheduleResource>> GetAllAsync()
        {
            var shedules = await _sheduleService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<Shedule>, IEnumerable<SheduleResource>>(shedules);

            return resorces;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _sheduleService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var instituteResource = _mapper.Map<Shedule, SheduleResource>(result.Resource);

            return Ok(instituteResource);
        }
        [HttpPost]
        [ProducesResponseType(typeof(SheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSheduleResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var shedule = _mapper.Map<SaveSheduleResource, Shedule>(resource);
            var result = await _sheduleService.SaveAsync(shedule);

            if (!result.Success)
                return BadRequest(result.Message);

            var sheduleResource = _mapper.Map<Shedule, SheduleResource>(result.Resource);
            return Ok(sheduleResource);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSheduleResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var shedule = _mapper.Map<SaveSheduleResource, Shedule>(resource);
            var result = await _sheduleService.UpdateAsync(id, shedule);

            if (!result.Success)
                return BadRequest(result.Message);

            var sheduleResource = _mapper.Map<Shedule, SheduleResource>(result.Resource);
            return Ok(sheduleResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _sheduleService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var sheduleResource = _mapper.Map<Shedule, SheduleResource>(result.Resource);
            return Ok(sheduleResource);
        }
    }
}
