using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveTutorReservationResource
    {
        [Required]
        public string StartDateTime { get; set; }

        [Required]
        public double TotalPrice { get; set; }
    }
}
