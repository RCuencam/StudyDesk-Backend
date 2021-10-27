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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all students")]
        [ProducesResponseType(typeof(IEnumerable<StudentResource>), 200)]
        public async Task<IEnumerable<StudentResource>> GetAllAsync()
        {
            var students = await _studentService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            return resorces;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a student by studentId")]
        [ProducesResponseType(typeof(StudentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _studentService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);

            return Ok(studentResource);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a student")]
        [ProducesResponseType(typeof(StudentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var student = _mapper.Map<SaveStudentResource, Student>(resource);
            var result = await _studentService.UpdateAsync(id, student);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);
            return Ok(studentResource);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a student")]
        [ProducesResponseType(typeof(StudentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _studentService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);
            return Ok(studentResource);
        }
    }
}
