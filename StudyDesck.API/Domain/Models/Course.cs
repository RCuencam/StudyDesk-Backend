using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
      
        public int CareerId { get; set; }
        public Career Career { get; set; }

        public IList<Tutor> Tutors { get; set; } = new List<Tutor>();

        public IList<Topic> Topics { get; set; } = new List<Topic>();

    }
}
