using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SavePlatformResource
    {
        [Required]
        public string UrlReunion { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
    }
}
