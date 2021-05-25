using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
    [Route("/api/student/{studentId}/materials")]
    public class StudentMaterialsController : ControllerBase
    {
        private readonly IStudentMaterialService _studentMaterialService;
        private readonly IMapper _mapper;

        public StudentMaterialsController(IStudentMaterialService studentMaterialService, IMapper mapper)
        {
            _studentMaterialService = studentMaterialService;
            _mapper = mapper;
        }

        [HttpGet] // to do: implement in controller StudentStudyMaterialsController -> 
        public async Task<IEnumerable<StudyMaterialResource>> GetAllStudyMaterialByStudentIdAsync(int studentId)
        {
            var result = await _studentMaterialService.ListByStudentIdAsync(studentId);
            var models = result.Select(s => s.StudyMaterial).ToList(); // BAD
            var resources = _mapper.Map<IEnumerable<StudyMaterial>, IEnumerable<StudyMaterialResource>>(models);
            return resources;
        }
    
        [HttpPost("{materialId}/categories/{categoryId}/institutes/{instituteId}")]
        public async Task<IActionResult> AssignStudentMaterialAsync
            (int studentId, long materialId, int categoryId, int instituteId)
        {
            var result = await _studentMaterialService
                .AssignStudentMaterialAsync(studentId, materialId, categoryId, instituteId);

            if (!result.Success)
                return BadRequest(result.Message);

            var materialResource = _mapper.Map<StudyMaterial, StudyMaterialResource>(result.Resource.StudyMaterial);

            return Ok(materialResource);
        }

        [HttpDelete("{materialId}")]
        public async Task<IActionResult> UnassignStudentMaterialAsync
            (int studentId, long materialId)
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
