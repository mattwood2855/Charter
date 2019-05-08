using System;
using System.Collections.Generic;
using System.Text;

namespace Charter.Models
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Violations { get; }

        public ValidationException(IEnumerable<string> violations)
        {
            Violations = violations;
        }
    }
}
