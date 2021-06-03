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
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionMaterialsController(ISessionMaterialService sessionMaterialService, IMapper mapper, IStudyMaterialService studyMaterialService, ISessionService sessionService)
        {
            _sessionMaterialService = sessionMaterialService;
            _mapper = mapper;
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<IEnumerable<SessionResource>> GetAllSessionMaterialsBySessionIdAsync(int tutorId)
        {
            var result = await _sessionService.ListByTutorIdAsync(tutorId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(result);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> AssignSessionMaterialAsync(int tutorId, [FromBody] SaveSessionMaterialResource resource)
        {
            var sessionMaterial = _mapper.Map<SaveSessionMaterialResource, SessionMaterial>(resource);
            var result = await _sessionMaterialService.AssignSessionMaterialAsync(tutorId, sessionMaterial);

            if (!result.Success)
                return BadRequest(result.Message);

            var materialResource = _mapper.Map<Session, SessionResource>(result.Resource.Session);

            return Ok(materialResource);
        }

        [HttpDelete("{materialId}")]
        public async Task<IActionResult> UnassignSessionMaterialAsync(int tutorId, int materialId)
        {
            var result = await _sessionMaterialService
                .UnassignSessionMaterialAsync(tutorId, materialId);

            if (!result.Success)
                return BadRequest(result.Message);

            var materialResource = _mapper.Map<Session, SessionResource>(result.Resource.Session);

            return Ok(materialResource);
        }



    }
}