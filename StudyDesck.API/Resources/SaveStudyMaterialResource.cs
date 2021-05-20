using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveStudyMaterialResource
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
