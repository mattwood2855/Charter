using System;
using System.Collections.Generic;
using System.Text;

namespace Charter.Models
{
    public class PasswordException : Exception
    {
        public IEnumerable<string> Violations { get; }

        public PasswordException(IEnumerable<string> violations)
        {
            Violations = violations;
        }
    }
}
