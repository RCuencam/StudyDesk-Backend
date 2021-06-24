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
    public class TutorReservationsController : ControllerBase
    {
        private readonly ITutorReservationService _tutorReservationService;
        private readonly IMapper _mapper;

        public TutorReservationsController(ITutorReservationService tutorReservationService, IMapper mapper)
        {
            _tutorReservationService = tutorReservationService;
            _mapper = mapper;
        }

        [HttpGet("students/{studentId}/tutors")]
        public async Task<IEnumerable<TutorReservationResource>> GetAllTutorsByStudentIdAsync(int studentId)
        {
            var result = await _tutorReservationService.ListByStudentIdAsync(studentId);
            var resources = _mapper.Map<IEnumerable<TutorReservation>, IEnumerable<TutorReservationResource>>(result);
            return resources;
        }


        [HttpGet("tutors/{tutorId}/students")]
        public async Task<IEnumerable<TutorReservationResource>> GetAllStudentsByTutorIdAsync(int tutorId)
        {
            var result = await _tutorReservationService.ListByTutorIdAsync(tutorId);
            var resources = _mapper.Map<IEnumerable<TutorReservation>, IEnumerable<TutorReservationResource>>(result);
            return resources;
        }

        [HttpPost("students/{studentId}/tutors/{tutorId}")]
        public async Task<IActionResult> createTutorReservationAsync(int studentId, int tutorId, [FromBody] SaveTutorReservationResource resource)
        {
            var tutorReservation = _mapper.Map<SaveTutorReservationResource, TutorReservation>(resource);
            var result = await _tutorReservationService.SaveTutorReservation(studentId, tutorId, tutorReservation);

            if (!result.Success)
                return BadRequest(result.Message);

            var materialResource = _mapper.Map<TutorReservation, TutorReservationResource>(result.Resource);

            return Ok(materialResource);
        }

        [HttpGet("tutors/{tutorId}/reservations")]
        public async Task<IEnumerable<TutorReservationResource>> GetAllTutorReservationsAsync(int tutorId)
        {
            var result = await _tutorReservationService.ListTutorReservationByTutorIdAsync(tutorId);
            var resources = _mapper.Map<IEnumerable<TutorReservation>, IEnumerable<TutorReservationResource>>(result);
            return resources;
        }

        [HttpPut("students/{studentId}/tutors/{tutorId}")]
        public async Task<IActionResult> UpdateTutorReservationAsync(int id, int studentId, int tutorId, [FromBody] SaveTutorReservationResource resource)
        {
            // studentId and tutorId are optionals
            var tutorReservation = _mapper.Map<SaveTutorReservationResource, TutorReservation>(resource);
            var result = await _tutorReservationService.UpdateTutorReservation(id, studentId, tutorId, tutorReservation);

            if (!result.Success)
                return BadRequest(result.Message);

            var materialResource = _mapper.Map<TutorReservation, TutorReservationResource>(result.Resource);

            return Ok(materialResource);
        }

    }
}
