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

    [ApiController]
    [Route("api/schedules")]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public SchedulesController(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List all schedules")]
        public async Task<IEnumerable<ScheduleResource>> GetAllAsync()
        {
            var results = await _scheduleService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleResource>>(results);
            return resources;
        }

    }
}
