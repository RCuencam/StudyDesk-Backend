using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class CareerResponse : BaseResponse<Career>
    {
        public CareerResponse(Career resource) : base(resource)
        {
        }

        public CareerResponse(string message) : base(message)
        {
        }
    }
}
