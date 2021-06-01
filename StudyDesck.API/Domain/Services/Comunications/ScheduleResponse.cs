using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class ScheduleResponse : BaseResponse<Schedule>
    {
        public ScheduleResponse(Schedule resource) : base(resource)
        {
        }

        public ScheduleResponse(string message) : base(message)
        {
        }
    }
}
