using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Reserved { get; set; }
        
        // relationships
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
    }
}
