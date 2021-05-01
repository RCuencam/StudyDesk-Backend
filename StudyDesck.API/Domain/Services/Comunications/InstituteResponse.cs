using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class InstituteResponse : BaseResponse<Institute>
    {
        public InstituteResponse(Institute resource) : base(resource)
        {
        }

        public InstituteResponse(string message) : base(message)
        {
        }

    }
}
