using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Shedule
    {
        public int Id { get; set; }
        public string StarDate { get; set; }
        public string EndDate { get; set; }
        public string Date { get; set; }
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
    }
}
