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
    [Route("/api/platforms/{platformId}/sessions")]
    [Produces("application/json")]
    public class PlatformSessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public PlatformSessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all sessions by platformId")]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllByPlatformIdAsync(int platformId)
        {
            var sessions = await _sessionService.ListByPlatformIdAsync(platformId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return resources;
        }
    }
}
