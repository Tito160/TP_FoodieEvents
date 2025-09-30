using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// IPersona.cs
namespace Tp_EventoComida.Interfaces
{
    public interface IPersona
    {
        int Id { get; }
        string NombreCompleto { get; }
        string Email { get; }
        string Telefono { get; }
        
        void Registrar();
        string ProporcionarDatosContacto();
        string PresentarInformacion();
    }
}