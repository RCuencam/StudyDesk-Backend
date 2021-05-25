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

        public long StudyMaterialId { get; set; }
        public StudyMaterial StudyMaterial { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int InstituteId { get; set; }
        public Institute Institute { get; set; }
    }
}
