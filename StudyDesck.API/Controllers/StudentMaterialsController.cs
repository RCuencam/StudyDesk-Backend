using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Resources;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
    [Route("/api/student/{studentId}/materials")]
    [ApiController]
    public class StudentMaterialsController : ControllerBase
    {
        private readonly IStudentMaterialService _studentMaterialService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly IMapper _mapper;

        public StudentMaterialsController(IStudentMaterialService studentMaterialService, IMapper mapper, IStudyMaterialService studyMaterialService)
        {
            _studentMaterialService = studentMaterialService;
            _mapper = mapper;
            _studyMaterialService = studyMaterialService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all materials of a student")]
        public async Task<IEnumerable<StudyMaterialResource>> GetAllStudyMaterialByStudentIdAsync(int studentId)
        {
            var result = await _studyMaterialService.ListByStudentIdAsync(studentId);
            var resources = _mapper.Map<IEnumerable<StudyMaterial>, IEnumerable<StudyMaterialResource>>(result);
            return resources;

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a material for a student")]
        public async Task<IActionResult> AssignStudentMaterialAsync(int studentId, [FromBody] SaveStudentMaterialResource resource)
        {
            var studentMaterial = _mapper.Map<SaveStudentMaterialResource, StudentMaterial>(resource);
            var result = await _studentMaterialService.AssignStudentMaterialAsync(studentId, studentMaterial);

            if (!result.Success)
                return BadRequest(result.Message);

            var materialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource.StudyMaterial);

            return Ok(materialResource);
        }

        [HttpDelete("{materialId}")]
        [SwaggerOperation(Summary = "Delete a material of a student")]
        public async Task<IActionResult> UnassignStudentMaterialAsync(int studentId, int materialId)
        {
            var result = await _studentMaterialService
                .UnassignStudentMaterialAsync(studentId, materialId);

            if (!result.Success)
                return BadRequest(result.Message);

            var materialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource.StudyMaterial);

            return Ok(materialResource);
        }

    }
}
