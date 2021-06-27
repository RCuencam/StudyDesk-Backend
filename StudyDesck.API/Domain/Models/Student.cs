using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int CareerId { get; set; }
        public Career Career { get; set; }

        public List<SessionReservation> SessionReservations { get; set; }
        public List<StudentMaterial> StudentMaterials { get; set; }
        public List<TutorReservation> TutorReservations { get; set; }

    }
}
