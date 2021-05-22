using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class SessionReservationResponse : BaseResponse<SessionReservation>
    {
        public SessionReservationResponse(SessionReservation resource) : base(resource)
        {
        }

        public SessionReservationResponse(string message) : base(message)
        {
        }
    }
}
