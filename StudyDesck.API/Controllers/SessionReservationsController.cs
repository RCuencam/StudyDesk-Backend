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
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class SessionReservationsController : ControllerBase
    {
        private readonly ISessionReservationService _sessionReservationService;
        private readonly IStudentService _studentService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

      
        public SessionReservationsController(ISessionReservationService sessionReservationService, IStudentService studentService, ISessionService sessionService, IMapper mapper)
        {
            _sessionReservationService = sessionReservationService;
            _studentService = studentService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List Students by SessionId",
            Description = "This endpoint returns all students that have the 'SessionId'",
            OperationId = "ListStudentsBySessionId"
        )]
        [SwaggerResponse(200, "List of Students by SessionId", typeof(IEnumerable<StudentResource>))]
        [HttpGet("{sessionId}/students")]
        [ProducesResponseType(typeof(IEnumerable<StudentResource>), 200)]
        public async Task<IEnumerable<StudentResource>> GetAllStudentsBySessionIdAsync(int sessionId)
        {
            var students = await _studentService.ListBySessionIdAsync(sessionId);
            var resources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            return resources;
        }

        [SwaggerOperation(
            Summary = "List Sessions by StudentId",
            Description = "This endpoint returns all sessions of a student",
            OperationId = "ListSessionsByStudentId"
        )]
        [SwaggerResponse(200, "List of sessions by StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpGet("{studentId}/sessions")]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllSessionsByStudentIdAsync(int studentId)
        {
            var sessions = await _sessionService.ListByStudentIdAsync(studentId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return resources;
        }


        [SwaggerOperation(
            Summary = "Post Session Reservation by SessionId and StudentId",
            Description = "This endpoint adds a session reservation between a session and a student",
            OperationId = "PostSessionReservationBySessionIdAndStudentId"
        )]
        [SwaggerResponse(200, "Post a session reservation by SessionId and StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpPost("{sessionId}/students/{studentId}")]
        [ProducesResponseType(typeof(IEnumerable<SessionReservationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int sessionId, int studentId, [FromBody] SaveSessionReservationResource resource)
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
           Summary = "Update Session Reservation by SessionId and StudentId",
           Description = "This endpoint updates a session reservation between a session and a student",
           OperationId = "UpdateSessionReservationBySessionIdAndStudentId"
       )]
        [SwaggerResponse(200, "Update a session reservation by SessionId and StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpPut("{sessionId}/students/{studentId}")]
        [ProducesResponseType(typeof(IEnumerable<SessionReservationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int sessionId, int studentId, [FromBody] SaveSessionReservationResource resource)
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
           Summary = "Delete Session Reservation by SessionId and StudentId",
           Description = "This endpoint removes a session reservation between a session and a student",
           OperationId = "DeleteSessionReservationBySessionIdAndStudentId"
       )]
        [SwaggerResponse(200, "Delete a session reservation by SessionId and StudentId", typeof(IEnumerable<SessionResource>))]
        [HttpDelete("{sessionId}/students/{studentId}")]
        [ProducesResponseType(typeof(IEnumerable<SessionReservationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int sessionId, int studentId)
        {
            var result = await _sessionReservationService.UnassignSessionReservationAsync(studentId,sessionId);
            if (!result.Success)
                return BadRequest(result.Message);
   
            var sessionReservationResource = _mapper.Map<SessionReservation, SessionReservationResource>(result.Resource);
            return Ok(sessionReservationResource);
        }

        
    }
}
