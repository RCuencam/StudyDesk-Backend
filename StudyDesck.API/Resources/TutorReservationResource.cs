using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class TutorReservationResource
    {
        public string StartDateTime { get; set; }
        public double TotalPrice { get; set; }
        public int Qualification { get; set; }
        public int TutorId { get; set; }
        public int StudentId { get; set; }
        public int PlatformId { get; set; }
    }
}
