using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class CareerResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public universityResource university { get; set; }
    }
}
