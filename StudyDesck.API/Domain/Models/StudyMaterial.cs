using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class StudyMaterial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double Size { get; set; }


        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        
        public IList<StudentMaterial> StudentMaterials { get; set; } = new List<StudentMaterial>();
        public IList<SessionMaterial> SessionMaterials { get; set; } = new List<SessionMaterial>();
    }
}
