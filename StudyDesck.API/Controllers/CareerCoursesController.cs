using AutoMapper;
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
    [Route("/api/career/{careerId}/courses")]
    [Produces("application/json")]
    public class CareerCoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CareerCoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "List Courses by categoryId")]
        [ProducesResponseType(typeof(IEnumerable<CourseResource>), 200)]
        public async Task<IEnumerable<CourseResource>> GetAllByCareerIdAsync(int careerId)
        {
            var courses = await _courseService.ListByCareerIdAsync(careerId);
            var resorces = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseResource>>(courses);

            return resorces;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get Course by Id, and CareerId")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int careerId, int id)
        {
            var result = await _courseService.GetByIdAsync(careerId, id);
            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);

            return Ok(courseResource);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a Course for a career")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int careerId, [FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var course = _mapper.Map<SaveCourseResource, Course>(resource);
            var result = await _courseService.SaveAsync(careerId, course);

            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "update a Course of a career")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int careerId, int id, [FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var course = _mapper.Map<SaveCourseResource, Course>(resource);
            var result = await _courseService.UpdateAsync(careerId, id, course);

            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a Course of a career")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int careerId, int id)
        {
            var result = await _courseService.DeleteAsync(careerId, id);
            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }
    }
}
