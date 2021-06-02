using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveSessionMaterialResource
    {
        public SaveStudyMaterialResource StudyMaterial { get; set; }
        public int TutorId { get; set; }
    }
}
