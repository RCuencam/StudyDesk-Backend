using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace StudyDesck.API.Extensions
{
    public static class StringExtentions
    {
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select
                    ( (x, i) => 
                        i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()
                    )
                ).ToLower();
        }
    }
}
