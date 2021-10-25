using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class StudentMaterialResource
    {
        public StudentResource Student { get; set; }
        public StudyMaterialResource StudyMaterial { get; set; }
    }
}
