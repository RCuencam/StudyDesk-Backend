using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string PlatformUrl { get; set; }
        public string Name { get; set; }

        // relationships
        public List<Session> Sessions { get; set; }
        public List<TutorReservation> TutorReservations { get; set; }

    }
}
