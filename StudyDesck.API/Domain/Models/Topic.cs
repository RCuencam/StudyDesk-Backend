﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Models
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public IList<ExpertTopic> ExpertTopics { get; set; } = new List<ExpertTopic>();
    }
}
