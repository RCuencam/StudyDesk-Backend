using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class TutorReservationResponse : BaseResponse<TutorReservation>
    {
        public TutorReservationResponse(TutorReservation resource) : base(resource)
        {
        }

        public TutorReservationResponse(string message) : base(message)
        {
        }
    }
}
