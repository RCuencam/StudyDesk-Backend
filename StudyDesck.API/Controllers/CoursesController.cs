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
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CoursesController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseResource>), 200)]
        public async Task<IEnumerable<CourseResource>> GetAllAsync()
        {
            var courses = await _courseService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseResource>>(courses);

            return resorces;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _courseService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);

            return Ok(courseResource);
        }


        [HttpPost]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var course = _mapper.Map<SaveCourseResource, Course>(resource);
            var result = await _courseService.SaveAsync(course);

            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCourseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var course = _mapper.Map<SaveCourseResource, Course>(resource);
            var result = await _courseService.UpdateAsync(id, course);

            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CourseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _courseService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var courseResource = _mapper.Map<Course, CourseResource>(result.Resource);
            return Ok(courseResource);
        }
    }
}
