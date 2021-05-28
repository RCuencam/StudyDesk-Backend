﻿using System;
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
        public string FileUrl { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        
        public IList<StudentMaterial> StudentMaterials { get; set; } = new List<StudentMaterial>();
        //public IList<SesionMaterial> SesionMaterials { get; set; } = new List<SesionMaterial>();
    }
}
