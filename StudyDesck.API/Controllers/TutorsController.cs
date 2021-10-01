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
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class TutorsController : ControllerBase
    {
        private readonly ITutorService _tutorService;
        private readonly IMapper _mapper;

        public TutorsController(ITutorService tutorService, IMapper mapper)
        {
            _tutorService = tutorService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all tutors")]
        [ProducesResponseType(typeof(IEnumerable<TutorResource>), 200)]
        public async Task<IEnumerable<TutorResource>> GetAllAsync()
        {
            var tutors = await _tutorService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Tutor>, IEnumerable<TutorResource>>(tutors);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "List a tutor by tutorId")]
        [ProducesResponseType(typeof(TutorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _tutorService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
            return Ok(tutorResource);
        }

        //[HttpPost]
        //[ProducesResponseType(typeof(TutorResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> PostAsync([FromBody] SaveTutorResource resource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    var tutor = _mapper.Map<SaveTutorResource, Tutor>(resource);
        //    var result = await _tutorService.SaveAsync(tutor);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
        //    return Ok(tutorResource);
        //}

        //[HttpPut("{id}")]
        //[ProducesResponseType(typeof(TutorResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> PutAsync(int id, [FromBody] SaveTutorResource resource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    var tutor = _mapper.Map<SaveTutorResource, Tutor>(resource);
        //    var result = await _tutorService.UpdateAsync(id, tutor);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var guardianResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
        //    return Ok(guardianResource);
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(TutorResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> DeleteAsync(int id)
        //{
        //    var result = await _tutorService.DeleteAsync(id);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var tutorResource = _mapper.Map<Tutor, TutorResource>(result.Resource);
        //    return Ok(tutorResource);
        //}
    }
}
