using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class TutorReservationResource
    {
        public int Id { get; set; }
        public TutorResource Tutor { get; set; }
        public StudentResource Student { get; set; }
        public string PlatformUrl { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public short Qualification { get; set; }
        public string Description { get; set; }
    }
}
