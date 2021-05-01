using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Extentions
{
    public static class ModelStateExtentions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(n => n.Value.Errors)
                .Select(m => m.ErrorMessage)
                .ToList();
        }
    }
}
