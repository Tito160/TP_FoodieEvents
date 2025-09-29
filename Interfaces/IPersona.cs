using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// IPersona.cs
namespace Tp_EventoComida
{
    /// Interfaz que define el contrato común para todas las personas del sistema.
    /// Garantiza que todas las personas tengan comportamientos básicos unificados.
    public interface IPersona
    {
        // 🔹 PROPIEDADES COMUNES - Todas las personas deben tener estos datos básicos
        int Id { get; }
        string NombreCompleto { get; }
        string Email { get; }
        string Telefono { get; }
        
        // 🔹 COMPORTAMIENTOS OBLIGATORIOS - Métodos que todas las personas deben implementar
        /// Registra a la persona en el sistema con lógica específica según el rol
        void Registrar();
        
        /// Proporciona información de contacto unificada
        string ProporcionarDatosContacto();
        /// Presenta información específica según el tipo de persona
        string PresentarInformacion();
    }
}