using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class UniversityResponse : BaseResponse<University>
    {
        public UniversityResponse(University resource) : base(resource)
        {
        }

        public UniversityResponse(string message) : base(message)
        {
        }

    }
}
