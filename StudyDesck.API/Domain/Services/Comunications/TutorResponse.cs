using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class TutorResponse : BaseResponse<Tutor>
    {
        public TutorResponse(Tutor resource) : base(resource)
        {
        }

        public TutorResponse(string message) : base(message)
        {
        }
    }
}
