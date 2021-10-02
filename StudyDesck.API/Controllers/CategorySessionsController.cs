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
    [Route("/api/categories/{categoryId}/sessions")]
    [Produces("application/json")]
    public class CategorySessionsController :ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        public CategorySessionsController(ISessionService sessionService, IMapper mapper)
        {
            _sessionService = sessionService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all sessions by categoryId")]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        public async Task<IEnumerable<SessionResource>> GetAllByTutorIdAsync(int categoryId)
        {
            var sessions = await _sessionService.ListByCategoryIdAsync(categoryId);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);

            return resources;
        }
    }
}
