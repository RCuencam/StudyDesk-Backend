using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class StudyMaterialResponse : BaseResponse<StudyMaterial>
    {
        public StudyMaterialResponse(StudyMaterial resource) : base(resource)
        {
        }

        public StudyMaterialResponse(string message) : base(message)
        {
        }
    }
}
