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
    [Route("/api/tutors/{tutorId}/sessions")]
    [Produces("application/json")]
    public class TutorSessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public TutorSessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllByTutorIdAsync(int tutorId)
        {
            var sessions = await _sessionService.ListByTutorIdAsync(tutorId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return resources;
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(SessionResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> GetAsync(int id)
        //{
        //    var result = await _sessionService.GetByIdAsync(id);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);

        //    return Ok(SessionResource);
        //}


        [HttpPost]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int tutorId,[FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.SaveAsync(tutorId,session);

            if (!result.Success)
                return BadRequest(result.Message);

            var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(SessionResource);
        }

        [HttpPut("{sessionId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int tutorId,int sessionId, [FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.UpdateAsync(tutorId,sessionId, session);

            if (!result.Success)
                return BadRequest(result.Message);

            var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(SessionResource);
        }

        [HttpDelete("{sessionId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int tutorId,int sessionId)
        {
            var result = await _sessionService.DeleteAsync(tutorId,sessionId);
            if (!result.Success)
                return BadRequest(result.Message);

            var SessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(SessionResource);
        }

    }
}
