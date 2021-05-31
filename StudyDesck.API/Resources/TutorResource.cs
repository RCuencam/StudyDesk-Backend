using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class TutorResource
    {
        public int Id { get; set; }
        public int CareerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double PricePerHour { get; set; }
    }
}
