using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveStudentResource
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(40)]
        public string Logo { get; set; }
        [Required]
        [MaxLength(40)]
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        public string Password { get; set; }

        [Required]
        public SaveCareerResource Career { get; set; }


    }
}
