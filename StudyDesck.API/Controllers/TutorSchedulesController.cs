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
    [Route("api/tutors/{tutorId}/schedules")]
    [Produces("application/json")]
    public class TutorSchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;
        public TutorSchedulesController(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all schedules of a tutor")]
        [ProducesResponseType(typeof(IEnumerable<ScheduleResource>), 200)]
        public async Task<IEnumerable<ScheduleResource>> GetAllByTutorIdAsync(int tutorId)
        {
            var schedules = await _scheduleService.ListByTutorIdAsync(tutorId);
            var resources = _mapper
                .Map<IEnumerable<Schedule>, IEnumerable<ScheduleResource>>(schedules);
            return resources;
        }

        [HttpGet("{scheduleId}")]
        [SwaggerOperation(Summary = "List a schedule of a tutor")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int scheduleId)
        {
            var result = await _scheduleService.GetByIdAsync(scheduleId);
            if (!result.Success)
                return BadRequest(result.Message);
            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a schedule for a tutor")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int tutorId, [FromBody] SaveScheduleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var schedule = _mapper.Map<SaveScheduleResource, Schedule>(resource);
            var result = await _scheduleService.SaveAsync(tutorId, schedule);

            if (!result.Success)
                return BadRequest(result.Message);

            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }

        [HttpPut("{scheduleId}")]
        [SwaggerOperation(Summary = "Uodate a schedule of a tutor")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int scheduleId, [FromBody] SaveScheduleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var schedule = _mapper.Map<SaveScheduleResource, Schedule>(resource);
            var result = await _scheduleService.UpdateAsync(scheduleId, schedule);

            if (!result.Success)
                return BadRequest(result.Message);

            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }

        [HttpDelete("{scheduleId}")]
        [SwaggerOperation(Summary = "Delete a schedule of a tutor")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int scheduleId)
        {
            var result = await _scheduleService.DeleteAsync(scheduleId);

            if (!result.Success)
                return BadRequest(result.Message);

            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }
    }
}
