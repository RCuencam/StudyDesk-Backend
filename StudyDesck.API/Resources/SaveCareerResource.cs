using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveCareerResource
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public int InstituteId { get; set; }
        //public SaveInstituteResource Institute { get; set; }
    }
}
