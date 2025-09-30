using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System;

namespace Tp_EventoComida
{
    public class ErrorValidacionException : Exception
    {
        public ErrorValidacionException() { }

        public ErrorValidacionException(string message) 
            : base(message) { }

        public ErrorValidacionException(string message, Exception inner) 
            : base(message, inner) { }
    }
}