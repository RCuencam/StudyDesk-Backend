using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class CourseResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CareerResource Career { get; set; }
    }
}
