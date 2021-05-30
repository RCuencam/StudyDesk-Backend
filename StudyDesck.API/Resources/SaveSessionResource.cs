using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveSessionResource
    {
        [Required]
        [MaxLength(15)]
        public string Title { get; set; }
        [Required]
        [MaxLength(30)]
        public string Logo { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Range(1, 5)]
        public int QuantityMembers { get; set; }
        [Required]
        [Range(0.0, 100.0)]
        public float Price { get; set; }

        ///Relaciones
        //[Required]
        //public int TutorID { get; set; }
        //[Required]
        //public TutorResource Tutor { get; set; } //Reemplazar

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int PlatformId { get; set; }
        [Required]
        public int TopicId { get; set; }
        //public CategoryResource Category { get; set; }

    }
}
