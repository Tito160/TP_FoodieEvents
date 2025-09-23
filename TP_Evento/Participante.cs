using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class Participante
{
    public int Id { get; private set; }
    public string NombreCompleto { get; private set; }
    public string Email { get; private set; }
    public string Telefono { get; private set; }
    public string DocumentoIdentidad { get; private set; }
    public string RestriccionAlimentaria { get; private set; }

    public Participante(int id, string nombreCompleto, string email, string telefono, string documentoIdentidad, string restriccionAlimentaria = "")
    {
        Id = id;
        NombreCompleto = nombreCompleto;
        Email = email;
        Telefono = telefono;
        DocumentoIdentidad = documentoIdentidad;
        RestriccionAlimentaria = restriccionAlimentaria;
    }

    public void ActualizarContacto(string nuevoEmail, string nuevoTelefono)
    {
        Email = nuevoEmail;
        Telefono = nuevoTelefono;
    }

    public override string ToString() => $"Participante: {NombreCompleto} ({DocumentoIdentidad})";
}
