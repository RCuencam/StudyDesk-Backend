using AutoMapper;
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
    [Route("/api/topics/{topicId}/sessions")]
    [Produces("application/json")]
    public class TopicSessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public TopicSessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all sessions of a topic")]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllByTopicIdAsync(int topicId)
        {
            var sessions = await _sessionService.ListByTopicIdAsync(topicId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return resources;
        }
    }
}
