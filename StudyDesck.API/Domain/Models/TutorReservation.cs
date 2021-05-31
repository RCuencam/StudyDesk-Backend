using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class TutorReservation
    {
        public int Id { get; set; }
        public string StartDateTime { get; set; }
        public double TotalPrice { get; set; }
        public int Qualification { get; set; }
        public Tutor Tutor { get; set; }
        public int TutorId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Platform Platform { get; set; }
        public int PlatformId { get; set; }
    }
}
