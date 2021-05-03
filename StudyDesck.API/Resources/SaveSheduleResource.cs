using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveSheduleResource
    {
        [Required]
        [MaxLength(30)]
        public string StarDate { get; set; }
        public string EndDate { get; set; }
        public string Date { get; set; }
    }
}
