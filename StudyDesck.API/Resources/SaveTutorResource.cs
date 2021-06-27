using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveTutorResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        [Required]
        [MaxLength(30)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        public double PricePerHour { get; set; }
    }
}
