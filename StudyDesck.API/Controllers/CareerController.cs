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
    [Produces("aplication/json")]
    public class CareerController : ControllerBase
    {
        private readonly ICareerService _careerService;
        private readonly IMapper _mapper;

        public CareerController(ICareerService careerService, IMapper mapper)
        {
            _careerService = careerService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CareerResource>), 200)]
        public async Task<IEnumerable<CareerResource>> GetAllAsync()
        {
            var careers = await _careerService.ListAsync();
            var resorces = _mapper.Map<IEnumerable<Career>, IEnumerable<CareerResource>>(careers);

            return resorces;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _careerService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var CareerResource = _mapper.Map<Career, CareerResource>(result.Resource);

            return Ok(CareerResource);
        }


        [HttpPost]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveCareerResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var career = _mapper.Map<SaveCareerResource, Career>(resource);
            var result = await _careerService.SaveAsync(career);

            if (!result.Success)
                return BadRequest(result.Message);

            var CareerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(CareerResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCareerResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var career = _mapper.Map<SaveCareerResource, Career>(resource);
            var result = await _careerService.UpdateAsync(id, career);

            if (!result.Success)
                return BadRequest(result.Message);

            var CareerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(CareerResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CareerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _careerService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var CareerResource = _mapper.Map<Career, CareerResource>(result.Resource);
            return Ok(CareerResource);
        }
    }
}
