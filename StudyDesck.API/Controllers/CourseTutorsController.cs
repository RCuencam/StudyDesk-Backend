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
    [Route("api/courses/{courseId}/tutors")]
    [Produces("application/json")]
    public class CourseTutorsController : ControllerBase
    {
        private readonly ITutorService _tutorService;
        private readonly IMapper _mapper;
        public CourseTutorsController(ITutorService tutorService, IMapper mapper)
        {
            _tutorService = tutorService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List Tutors by courseId")]
        [ProducesResponseType(typeof(IEnumerable<TutorResource>), 200)]
        public async Task<IEnumerable<TutorResource>> GetAllByCourseIdAsync(int courseId)
        {
            var tutors = await _tutorService.ListByCourseIdAsync(courseId);
            var resources = _mapper
                .Map<IEnumerable<Tutor>, IEnumerable<TutorResource>>(tutors);
            return resources;
        }

        [HttpGet("{tutorId}")]
        [SwaggerOperation(Summary = "Get a Tutor by courseId and tutorId")]
        [ProducesResponseType(typeof(TutorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int courseId, int tutorId)
        {
            var result = await _tutorService.GetByCourseIdandTutorIdAsync(courseId, tutorId);
            if (!result.Success)
                return BadRequest(result.Message);
            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
            return Ok(tutorResource);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a Tutor for a Course")]
        [ProducesResponseType(typeof(TutorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int courseId, [FromBody] SaveTutorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tutor = _mapper.Map<SaveTutorResource, Tutor>(resource);
            var result = await _tutorService.SaveAsync(courseId, tutor);

            if (!result.Success)
                return BadRequest(result.Message);

            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
            return Ok(tutorResource);
        }

        [HttpPut("{tutorId}")]
        [SwaggerOperation(Summary = "Update a tutor of a Course")]
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
        [SwaggerOperation(Summary = "Delete a tutor of a course")]
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
