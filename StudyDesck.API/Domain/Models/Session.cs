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

        ///Relaciones
        public int TutorID { get; set; }
        public Tutor Tutor { get; set; } //Reemplazar
        public int CategoryID { get; set; } 
        public Category Category { get; set; }
        public IList<Topic> Topics { get; set; } = new List<Topic>();
        public List<SessionReservation> SessionReservations { get; set; }

    }
}
