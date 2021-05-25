using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Institute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Career> Careers { get; set; } = new List<Career>();
        public IList<StudentMaterial> StudentMaterials { get; set; } = new List<StudentMaterial>();
    }
}
