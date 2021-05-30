using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SessionResource
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
        //public int TutorID { get; set; }
        public TutorResource Tutor { get; set; } 
        //public int CategoryID { get; set; }
        public CategoryResource Category { get; set; }
        public PlatformResource Platform { get; set; }
        public TopicResource Topic { get; set; }
    }
}
