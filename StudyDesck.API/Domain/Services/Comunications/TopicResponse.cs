using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class TopicResponse : BaseResponse<Topic>
    {
        public TopicResponse(Topic resource) : base(resource)
        {
        }

        public TopicResponse(string message) : base(message)
        {
        }
    }
}
