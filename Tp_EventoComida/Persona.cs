using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp_EventoComida;

// Persona.cs
namespace Tp_EventoComida
{
    /// Clase abstracta que proporciona implementación base común para todas las personas.
    /// Implementa la interfaz IPersona y define comportamiento común.
    public abstract class Persona : IPersona
    {
        // 🔹 PROPIEDADES COMUNES - Implementación de la interfaz
        public int Id { get; protected set; }
        public string NombreCompleto { get; protected set; }
        public string Email { get; protected set; }
        public string Telefono { get; protected set; }

        /// Constructor protegido que valida datos comunes y establece propiedades base
        protected Persona(int id, string nombreCompleto, string email, string telefono)
        {
            // ✅ VALIDACIONES COMUNES - Aplicadas a todas las personas
            ValidadorDatos.ValidarEmail(email);
            ValidadorDatos.ValidarTelefono(telefono);

            // 🏗️ INICIALIZACIÓN DE PROPIEDADES BASE
            Id = id;
            NombreCompleto = nombreCompleto;
            Email = email;
            Telefono = telefono;
        }

        // 🔹 COMPORTAMIENTOS COMUNES - Implementaciones base

        /// Implementación base del registro - común para todas las personas
        /// Las clases derivadas pueden extender este comportamiento
        public virtual void Registrar()
        {
            // 📝 Comportamiento común de registro
            Console.WriteLine($"{NombreCompleto} se ha registrado en el sistema.");
        }

        /// Proporciona datos de contacto en formato unificado
        public virtual string ProporcionarDatosContacto()
        {
            return $"Contacto: {Email} | Tel: {Telefono}";
        }

        // 🔹 COMPORTAMIENTO ABSTRACTO - Obliga a las clases derivadas a implementar su lógica específica
        
        /// Método abstracto que cada tipo de persona debe implementar
        /// para mostrar información específica de su rol
        public abstract string PresentarInformacion();

        // 🔹 MÉTODOS UTILITARIOS COMUNES

        /// Permite actualizar datos de contacto con validaciones
        public virtual void ActualizarContacto(string nuevoEmail, string nuevoTelefono)
        {
            // ✅ Validaciones antes de actualizar
            ValidadorDatos.ValidarEmail(nuevoEmail);
            ValidadorDatos.ValidarTelefono(nuevoTelefono);
            
            // ✏️ Actualización de propiedades
            Email = nuevoEmail;
            Telefono = nuevoTelefono;
        }

        /// Representación string común para todas las personas
        public override string ToString()
        {
            return $"{NombreCompleto} ({Email})";
        }
    }
}