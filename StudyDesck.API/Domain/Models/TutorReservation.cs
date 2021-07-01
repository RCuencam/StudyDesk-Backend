using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class TutorReservation
    {
        public int Id { get; set; }
        public Tutor Tutor { get; set; }
        public int TutorId { get; set; }
        
        public Student Student { get; set; }
        public int StudentId { get; set; }

        public string PlatformUrl { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public short Qualification { get; set; }
        public string Description { get; set; }  
        public bool Confirmed { get; set; }
    }
}
