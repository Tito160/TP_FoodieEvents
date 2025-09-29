using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida
{
    // Excepción personalizada para errores de validación
    public class ErrorValidacionException : Exception
    {
        public ErrorValidacionException() { }

        public ErrorValidacionException(string message) 
            : base(message) { }

        public ErrorValidacionException(string message, Exception inner) 
            : base(message, inner) { }
    }
}
