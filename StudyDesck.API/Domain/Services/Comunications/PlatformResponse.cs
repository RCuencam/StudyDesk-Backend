using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class PlatformResponse : BaseResponse<Platform>
    {
        public PlatformResponse(Platform resource) : base(resource)
        {
        }

        public PlatformResponse(string message) : base(message)
        {
        }
    }
}
