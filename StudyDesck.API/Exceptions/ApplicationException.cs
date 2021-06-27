using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StudyDesck.API.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException() : base() { }

        public ApplicationException(string message) : base(message) { }

        public ApplicationException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
