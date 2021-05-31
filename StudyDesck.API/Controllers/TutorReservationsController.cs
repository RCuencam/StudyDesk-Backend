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
    public class TutorReservationsController : ControllerBase
    {
        private readonly ITutorReservationService _tutorReservationService;
        private readonly IStudentService _studentService;
        private readonly ITutorService _tutorService;
        private readonly IPlatformService _platformService;
        private readonly IMapper _mapper;

        public TutorReservationsController(ITutorReservationService tutorReservationService, IStudentService studentService, ITutorService tutorService, IPlatformService platformService, IMapper mapper)
        {
            _tutorReservationService = tutorReservationService;
            _studentService = studentService;
            _tutorService = tutorService;
            _platformService = platformService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TutorReservationResource>> GetAllAsync()
        {

            var result = await _tutorReservationService.ListAsync();
            var resources = _mapper.Map<IEnumerable<TutorReservation>, IEnumerable<TutorReservationResource>>(result);
            return resources;
        }

        [HttpPost("{studentId}/{tutorId}/{platformId}")]
        public async Task<IActionResult> PostAsync(int studentId, int tutorId, int platformId, [FromBody] SaveTutorReservationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tutorReservation = _mapper.Map<SaveTutorReservationResource, TutorReservation>(resource);

            var result = await _tutorReservationService.AssignTutorReservationAsync(studentId, tutorId, platformId, tutorReservation);

            if (!result.Success)
                return BadRequest(result.Message);

            var tutorReservationResource = _mapper.Map<TutorReservation, TutorReservationResource>(result.Resource);

            return Ok(tutorReservationResource);
        }

        [HttpPut("{studentId}/{tutorId}/{platformId}")]
        public async Task<IActionResult> PutAsync(int studentId, int tutorId, int platformId, [FromBody] SaveTutorReservationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var tutorReservation = _mapper.Map<SaveTutorReservationResource, TutorReservation>(resource);

            var result = await _tutorReservationService.UpdateTutorReservationAsync(studentId, tutorId, platformId, tutorReservation);
            if (!result.Success)
                return BadRequest(result.Message);

            var tutorReservationResource = _mapper.Map<TutorReservation, TutorReservationResource>(result.Resource);
            return Ok(tutorReservationResource);
        }

        [HttpDelete("{studentId}/{tutorId}/{platformId}")]
        public async Task<IActionResult> DeleteAsync(int studentId, int tutorId, int platformId)
        {
            var result = await _tutorReservationService.UnassignTutorReservationAsync(studentId, tutorId, platformId);
            if (!result.Success)
                return BadRequest(result.Message);

            var tutorReservationResource = _mapper.Map<TutorReservation, TutorReservationResource>(result.Resource);
            return Ok(tutorReservationResource);
        }
    }
}
