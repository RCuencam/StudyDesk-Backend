using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    [Route("/api/sessions/{sessionId}/students")]
    [Produces("application/json")]
    public class SessionStudentsController : ControllerBase
    {
        private readonly ISessionReservationService _sessionReservationService;
        private readonly IStudentService _studentService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionStudentsController(ISessionReservationService sessionReservationService, IStudentService studentService, ISessionService sessionService, IMapper mapper)
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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentResource>), 200)]
        public async Task<IEnumerable<StudentResource>> GetAllStudentsBySessionIdAsync(int sessionId)
        {
            var students = await _studentService.ListBySessionIdAsync(sessionId);
            var resources = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentResource>>(students);

            return resources;
        }
    }
}
