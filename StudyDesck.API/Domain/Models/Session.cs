using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int QuantityMembers { get; set; }
        public float Price { get; set; }

        // Relationships
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        
        public int PlataformId { get; set; }
        public Platform Platform { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        public int CategoryId { get; set; } 
        public Category Category { get; set; }
        
        
        public List<SessionReservation> SessionReservations { get; set; }

    }
}
