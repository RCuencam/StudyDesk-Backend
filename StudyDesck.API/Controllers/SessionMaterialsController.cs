using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudyDesck.API.Domain.Models;
using StudyDesck.API.Domain.Services;
using StudyDesck.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Controllers
{
    [Route("/api/session/{sessionId}/materials")]
    public class SessionMaterialsController : ControllerBase
    {
        private readonly ISessionMaterialService _sessionMaterialService;
        private readonly IStudyMaterialService _studyMaterialService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionMaterialsController(ISessionMaterialService sessionMaterialService, IMapper mapper, IStudyMaterialService studyMaterialService, ISessionService sessionService)
        {
            _sessionMaterialService = sessionMaterialService;
            _mapper = mapper;
            _studyMaterialService = studyMaterialService;
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IEnumerable<SessionMaterialResource>> GetAllSessionMaterialsBySessionIdAsync(int sessionId)
        {
            var result = await 
        }


    }
}
