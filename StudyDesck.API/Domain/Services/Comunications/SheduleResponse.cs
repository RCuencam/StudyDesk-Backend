using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class SheduleResponse : BaseResponse<Shedule>
    {
        public SheduleResponse(Shedule resource) : base(resource)
        {
        }

        public SheduleResponse(string message) : base(message)
        {
        }
    }
}
