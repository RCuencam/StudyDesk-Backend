using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveStudentMaterialResource
    {
        public SaveStudyMaterialResource StudyMaterial { get; set; }
        public int CategoryId { get; set; } 
        public int InstituteId { get; set; }
    }
}
