using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveCategoryResource
    {
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        //public IList<Session> Sessions { get; set; } = new List<Session>();

    }
}
