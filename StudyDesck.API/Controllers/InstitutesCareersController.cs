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
    [Route("/api")]
    [Produces("application/json")]
    public class InstitutesCareersController : ControllerBase
    {
        private readonly IInstituteService _instituteService;
        private readonly ICareerService _careerService;
        private readonly IMapper _mapper;

        public InstitutesCareersController(IInstituteService instituteService, ICareerService careerService, IMapper mapper)
        {
            _instituteService = instituteService;
            _careerService = careerService;
            _mapper = mapper;
        }

        [HttpGet("institutes/{instituteId}/careers")]
        public async Task<IEnumerable<CareerResource>> GetAllByInstituteIdAsync(int instituteId)
        {
            var careers = await _careerService.FindByInstituteId(instituteId);
            var resources = _mapper.Map<IEnumerable<Career>, IEnumerable<CareerResource>>(careers);

            return resources;
        }

        [HttpGet("institutes/{instituteId}/careers/{careerId}")]
        public async Task<IEnumerable<CareerResource>> GetAllByInstituteIdAndCareerIdAsync(int instituteId, int careerId)
        {
            var careers = await _careerService.GetByInstituteIdAndCareerId(instituteId, careerId);
            var resources = _mapper.Map<IEnumerable<Career>, IEnumerable<CareerResource>>(careers);

            return resources;
        }

        [HttpPost("institutes/{instituteId}/careers")]

        public async Task<IActionResult> PostAsync(int instituteId,[FromBody] SaveCareerResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var career = _mapper.Map<SaveCareerResource, Career>(resource);
            var result = await _careerService.SaveAsync(instituteId,career);

            if (!result.Success)
                return BadRequest(result.Message);

            var careerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(careerResource);
        }

        [HttpDelete("institutes/{instituteId}/careers/{careerId}")]
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
