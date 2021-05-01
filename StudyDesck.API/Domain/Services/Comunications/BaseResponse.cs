using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Domain.Services.Comunications
{
    public abstract class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; protected set; }
        public T Resource { get; set; }

        public BaseResponse(T resource)
        {
            Resource = resource;
            Success = true;
            Message = string.Empty;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
