using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class ExpertTopicResponse : BaseResponse<ExpertTopic>
    {
        public ExpertTopicResponse(ExpertTopic resource) : base(resource)
        {
        }

        public ExpertTopicResponse(string message) : base(message)
        {
        }
    }
}
