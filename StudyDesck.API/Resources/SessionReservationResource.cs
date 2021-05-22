using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SessionReservationResource
    {
        public int Qualification { get; set; }
        public bool Confirmed { get; set; }
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        //public SessionResource Session { get; set; }
        //public StudentResource Student { get; set; }
        
    }
}
