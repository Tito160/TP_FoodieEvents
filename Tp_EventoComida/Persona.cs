using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// Persona.cs
namespace Tp_EventoComida
{
    public abstract class Persona : IPersona
    {
        public int Id { get; protected set; }
        public string NombreCompleto { get; protected set; }
        public string Email { get; protected set; }
        public string Telefono { get; protected set; }

        protected Persona(int id, string nombreCompleto, string email, string telefono)
        {
            ValidadorDatos.ValidarEmail(email);
            ValidadorDatos.ValidarTelefono(telefono);

            Id = id;
            NombreCompleto = nombreCompleto;
            Email = email;
            Telefono = telefono;
        }

        public virtual void Registrar()
        {
            Console.WriteLine($"{NombreCompleto} se ha registrado en el sistema.");
        }

        public virtual string ProporcionarDatosContacto()
        {
            return $"Contacto: {Email} | Tel: {Telefono}";
        }

        public abstract string PresentarInformacion();

        public virtual void ActualizarContacto(string nuevoEmail, string nuevoTelefono)
        {
            ValidadorDatos.ValidarEmail(nuevoEmail);
            ValidadorDatos.ValidarTelefono(nuevoTelefono);

            Email = nuevoEmail;
            Telefono = nuevoTelefono;
        }

        public override string ToString()
        {
            return $"{NombreCompleto} ({Email})";
        }
    }
}