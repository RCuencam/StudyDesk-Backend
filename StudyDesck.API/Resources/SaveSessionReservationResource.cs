using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Resources
{
    public class SaveSessionReservationResource
    {
       [Range(0,5)]
       public int Qualification { get; set; }
       public bool Confirmed { get; set; }
    }
}
