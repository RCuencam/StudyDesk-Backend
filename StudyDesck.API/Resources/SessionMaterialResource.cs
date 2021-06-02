using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SessionMaterialResource
    {
        public int SessionId { get; set; }
        public long StudyMaterialId { get; set; }
        public int TutorId { get; set; }
    }
}
