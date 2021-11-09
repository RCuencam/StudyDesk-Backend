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
    [Route("/api")]
    [Produces("application/json")]
    public class UniversityCareersController : ControllerBase
    {
        private readonly IUniversityService _universityService;
        private readonly ICareerService _careerService;
        private readonly IMapper _mapper;

        public UniversityCareersController(IUniversityService universityService, ICareerService careerService, IMapper mapper)
        {
            _universityService = universityService;
            _careerService = careerService;
            _mapper = mapper;
        }

        [HttpGet("universities/{universityId}/careers")]
        [SwaggerOperation(Summary = "List all careers of an university")]
        public async Task<IEnumerable<CareerResource>> GetAllByuniversityIdAsync(int universityId)
        {
            var careers = await _careerService.FindByuniversityId(universityId);
            var resources = _mapper.Map<IEnumerable<Career>, IEnumerable<CareerResource>>(careers);

            return resources;
        }

        [HttpGet("universities/{universityId}/careers/{careerId}")]
        [SwaggerOperation(Summary = "List a career of an university")]
        public async Task<IEnumerable<CareerResource>> GetAllByuniversityIdAndCareerIdAsync(int universityId, int careerId)
        {
            var careers = await _careerService.GetByuniversityIdAndCareerId(universityId, careerId);
            var resources = _mapper.Map<IEnumerable<Career>, IEnumerable<CareerResource>>(careers);

            return resources;
        }

        [HttpPost("universities/{universityId}/careers")]
        [SwaggerOperation(Summary = "Create a career for an university")]
        public async Task<IActionResult> PostAsync(int universityId,[FromBody] SaveCareerResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var career = _mapper.Map<SaveCareerResource, Career>(resource);
            var result = await _careerService.SaveAsync(universityId,career);

            if (!result.Success)
                return BadRequest(result.Message);

            var careerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(careerResource);
        }

        [HttpDelete("universities/{universityId}/careers/{careerId}")]
        [SwaggerOperation(Summary = "Delete a career of an university")]
        public async Task<IActionResult> DeleteAsync(int careerId)
        {
            var result = await _careerService.DeleteAsync(careerId);
            if (!result.Success)
                return BadRequest(result.Message);

            var careerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(careerResource);
        }

    }
}
