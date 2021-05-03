using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Tutor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string InstituteName { get; set; }
        public List<ExpertTopic> ExpertTopics { get; set; }
        public List<Shedule> Shedules { get; set; }
        public List<Session> Sessions { get; set; }
        //public List<SessionMaterial> SessionMaterials { get; set; }
    }
}
