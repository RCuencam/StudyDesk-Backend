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
    [Route("/api/careers/{careerId}/students")]
    [Produces("application/json")]
    public class CareerStudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public CareerStudentsController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(StudentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int careerId,[FromBody] SaveStudentResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var student = _mapper.Map<SaveStudentResource, Student>(resource);
            var result = await _studentService.SaveAsync(careerId,student);

            if (!result.Success)
                return BadRequest(result.Message);

            var studentResource = _mapper.Map<Student, StudentResource>(result.Resource);
            return Ok(studentResource);
        }
    }
}
