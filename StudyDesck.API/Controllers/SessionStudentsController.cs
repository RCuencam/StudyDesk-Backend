using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
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
    }
}
