using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class SessionReservation
    {
        public int StudentId { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }
        public Student Student { get; set; }
        public int Qualification { get; set; }
        public bool Confirmed { get; set; }
    }
}
