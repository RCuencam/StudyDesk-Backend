using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class SessionMaterial
    {
        public Session Session { get; set; }
        public int SessionId { get; set; }
        public StudyMaterial StudyMaterial { get; set; }
        public int StudyMaterialId { get; set; }
        public Tutor Tutor { get; set; }
        public int TutorId { get; set; }
    }
}
