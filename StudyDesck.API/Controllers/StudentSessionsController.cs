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
    //Cuales son los estudiantes asociados a una session
    [ApiController]
    [Route("/api/students/{studentId}/sessions")]
    [Produces("application/json")]
    public class StudentSessionsController : ControllerBase
    {
        private readonly ISessionReservationService _sessionReservationService;
        private readonly IStudentService _studentService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

      
        public StudentSessionsController(ISessionReservationService sessionReservationService, IStudentService studentService, ISessionService sessionService, IMapper mapper)
        {
            _sessionReservationService = sessionReservationService;
            _studentService = studentService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        

        [SwaggerOperation(
            Summary = "List Sessions by StudentId",
            Description = "This endpoint returns all sessions of a student",
            OperationId = "ListSessionsByStudentId"
        )]
        [SwaggerResponse(200, "List of sessions by StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllSessionsByStudentIdAsync(int studentId)
        {
            var sessions = await _sessionService.ListByStudentIdAsync(studentId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return resources;
        }


        [SwaggerOperation(
            Summary = "Post Session Reservation by StudentId and SessionId",
            Description = "This endpoint adds a session reservation between a session and a student",
            OperationId = "PostSessionReservationBySessionIdAndStudentId"
        )]
        [SwaggerResponse(200, "Post a session reservation by SessionId and StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpPost("{sessionId}")]
        [ProducesResponseType(typeof(IEnumerable<SessionReservationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int studentId, int sessionId, [FromBody] SaveSessionReservationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var sessionReservation = _mapper.Map<SaveSessionReservationResource, SessionReservation>(resource);

            var result = await _sessionReservationService.AssignSessionReservationAsync(studentId, sessionId, sessionReservation);

            if(!result.Success)
                return BadRequest(result.Message);

            var sessionReservationResource = _mapper.Map<SessionReservation, SessionReservationResource>(result.Resource);

            return Ok(sessionReservationResource);
        }


        [SwaggerOperation(
           Summary = "Update Session Reservation by StudentId and SessionId",
           Description = "This endpoint updates a session reservation between a session and a student",
           OperationId = "UpdateSessionReservationBySessionIdAndStudentId"
       )]
        [SwaggerResponse(200, "Update a session reservation by SessionId and StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpPut("{sessionId}")]
        [ProducesResponseType(typeof(IEnumerable<SessionReservationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int studentId, int sessionId, [FromBody] SaveSessionReservationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var sessionReservation = _mapper.Map<SaveSessionReservationResource, SessionReservation>(resource);

            var result = await _sessionReservationService.UpdateSessionReservationAsync(studentId, sessionId, sessionReservation);
            if (!result.Success)
                return BadRequest(result.Message);

            var sessionReservationResource = _mapper.Map<SessionReservation, SessionReservationResource>(result.Resource);
            return Ok(sessionReservationResource);
        }



        [SwaggerOperation(
           Summary = "Delete Session Reservation by StudentId and SessionId",
           Description = "This endpoint removes a session reservation between a session and a student",
           OperationId = "DeleteSessionReservationBySessionIdAndStudentId"
       )]
        [SwaggerResponse(200, "Delete a session reservation by SessionId and StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpDelete("{sessionId}")]
        [ProducesResponseType(typeof(IEnumerable<SessionReservationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int studentId, int sessionId)
        {
            var result = await _sessionReservationService.UnassignSessionReservationAsync(studentId,sessionId);
            if (!result.Success)
                return BadRequest(result.Message);
   
            var sessionReservationResource = _mapper.Map<SessionReservation, SessionReservationResource>(result.Resource);
            return Ok(sessionReservationResource);
        }

        
    }
}
