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
    [Route("api/careers/{careerId}/tutors")]
    public class CareerTutorsController : ControllerBase
    {
        private readonly ITutorService _tutorService;
        private readonly IMapper _mapper;
        public CareerTutorsController(ITutorService tutorService, IMapper mapper)
        {
            _tutorService = tutorService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TutorResource>), 200)]
        public async Task<IEnumerable<TutorResource>> GetAllByCareerIdAsync(int careerId)
        {
            var tutors = await _tutorService.ListByCareerIdAsync(careerId);
            var resources = _mapper
                .Map<IEnumerable<Tutor>, IEnumerable<TutorResource>>(tutors);
            return resources;
        }

        [HttpGet("{tutorId}")]
        [ProducesResponseType(typeof(TutorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int tutorId)
        {
            var result = await _tutorService.GetByIdAsync(tutorId);
            if (!result.Success)
                return BadRequest(result.Message);
            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
            return Ok(tutorResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TutorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int careerId, [FromBody] SaveTutorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tutor = _mapper.Map<SaveTutorResource, Tutor>(resource);
            var result = await _tutorService.SaveAsync(careerId, tutor);

            if (!result.Success)
                return BadRequest(result.Message);

            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
            return Ok(tutorResource);
        }

        [HttpPut("{tutorId}")]
        [ProducesResponseType(typeof(TutorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int tutorId, [FromBody] SaveTutorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tutor = _mapper.Map<SaveTutorResource, Tutor>(resource);
            var result = await _tutorService.UpdateAsync(tutorId, tutor);

            if (!result.Success)
                return BadRequest(result.Message);

            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
            return Ok(tutorResource);
        }

        [HttpDelete("{tutorId}")]
        [ProducesResponseType(typeof(TutorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int tutorId)
        {
            var result = await _tutorService.DeleteAsync(tutorId);

            if (!result.Success)
                return BadRequest(result.Message);

            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
            return Ok(tutorResource);
        }
    }
}
