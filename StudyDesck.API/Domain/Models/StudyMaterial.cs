using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class StudyMaterial
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long TopicId { get; set; }
        public Topic Topic { get; set; }
        //public IList<SesionMaterial> SesionMaterials { get; set; } = new List<SesionMaterial>();
        //public IList<StudentMaterial> StudentMaterials { get; set; } = new List<StudentMaterial>();
    }
}
