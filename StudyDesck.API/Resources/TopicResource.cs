using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class TopicResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseResource Course { get; set; }
    }
}
