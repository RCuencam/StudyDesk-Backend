using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveScheduleResource
    {
        [Required]
        public DateTime StarDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
