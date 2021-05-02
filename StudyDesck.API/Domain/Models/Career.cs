using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Career
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InstituteId { get; set; }
        public Institute Institute { get; set; }

        public IList<Student> Students { get; set; } = new List<Student>();
        public IList<Course> Courses { get; set; } = new List<Course>();
    }
}
