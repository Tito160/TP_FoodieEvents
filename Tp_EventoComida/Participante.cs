using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida;

using System;

namespace Tp_EventoComida
{
    public class Participante
    {
        public int Id { get; private set; }
        public string NombreCompleto { get; private set; }
        public string Email { get; private set; }
        public string Telefono { get; private set; }
        public string DocumentoIdentidad { get; private set; }
        public string RestriccionAlimentaria { get; private set; }

        // Constructor con validaciones
        public Participante(int id, string nombreCompleto, string email, string telefono, string documentoIdentidad, string restriccionAlimentaria = "")
        {
            // Validaciones
            ValidadorDatos.ValidarEmail(email);
            ValidadorDatos.ValidarTelefono(telefono);

            // Asignación de propiedades
            Id = id;
            NombreCompleto = nombreCompleto;
            Email = email;
            Telefono = telefono;
            DocumentoIdentidad = documentoIdentidad;
            RestriccionAlimentaria = restriccionAlimentaria;
        }

        // Método para actualizar contacto con validaciones
        public void ActualizarContacto(string nuevoEmail, string nuevoTelefono)
        {
            ValidadorDatos.ValidarEmail(nuevoEmail);
            ValidadorDatos.ValidarTelefono(nuevoTelefono);

            Email = nuevoEmail;
            Telefono = nuevoTelefono;
        }

        // Resumen del participante
        public override string ToString()
        {
            return $"Participante: {NombreCompleto} ({DocumentoIdentidad}) | Email: {Email} | Tel: {Telefono}";
        }
    }
}
