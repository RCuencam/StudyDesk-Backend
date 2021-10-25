using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class StudentMaterial
    {
        public int StudentId { get; set; }
        public Student student { get; set; }

        public int StudyMaterialId { get; set; }
        public StudyMaterial StudyMaterial { get; set; }
    }
}
