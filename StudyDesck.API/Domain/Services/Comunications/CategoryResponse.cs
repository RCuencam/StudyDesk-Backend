using StudyDesck.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public class CategoryResponse : BaseResponse<Category>
    {
        public CategoryResponse(Category resource) : base(resource)
        {
        }

        public CategoryResponse(string message) : base(message)
        {
        }
    }
}
