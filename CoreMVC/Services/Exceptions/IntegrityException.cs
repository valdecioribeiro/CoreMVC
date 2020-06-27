
using System;

namespace CoreMVC.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message) { }
    }
}
