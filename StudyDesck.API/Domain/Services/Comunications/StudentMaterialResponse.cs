using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class StudentMaterialResponse : BaseResponse<StudentMaterial>
    {
        public StudentMaterialResponse(StudentMaterial resource) : base(resource)
        {
        }

        public StudentMaterialResponse(string message) : base(message)
        {
        }
    }
}
