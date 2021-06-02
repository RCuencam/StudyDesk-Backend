using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class SessionMaterialResponse : BaseResponse<SessionMaterial>
    {
        public SessionMaterialResponse(SessionMaterial resource) : base(resource)
        {
        }

        public SessionMaterialResponse(string message) : base(message)
        {
        }
    }
}
