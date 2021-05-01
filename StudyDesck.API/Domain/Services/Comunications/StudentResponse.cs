using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class StudentResponse : BaseResponse<Student>
    {
        public StudentResponse(Student resource) : base(resource)
        {
        }

        public StudentResponse(string message) : base(message)
        {
        }
    }
}
