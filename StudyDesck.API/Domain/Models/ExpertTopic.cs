using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class ExpertTopic
    {
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
