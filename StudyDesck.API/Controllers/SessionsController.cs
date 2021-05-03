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
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public SessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllAsync()
        {
            var sessions = await _sessionService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _sessionService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);

            return Ok(SessionResource);
        }


        [HttpPost]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.SaveAsync(session);

            if (!result.Success)
                return BadRequest(result.Message);

            var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(SessionResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.UpdateAsync(id, session);

            if (!result.Success)
                return BadRequest(result.Message);

            var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(SessionResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _sessionService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(SessionResource);
        }

    }
}
