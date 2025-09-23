using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tp_EventoComida;

public class Chef
{
    public int Id { get; private set; }
    public string NombreCompleto { get; private set; }
    public string Especialidad { get; private set; }
    public string Nacionalidad { get; private set; }
    public int AniosExperiencia { get; private set; }
    public string Email { get; private set; }
    public string Telefono { get; private set; }

    public Chef(int id, string nombreCompleto, string especialidad, string nacionalidad, int aniosExperiencia, string email, string telefono)
    {
        Id = id;
        NombreCompleto = nombreCompleto;
        Especialidad = especialidad;
        Nacionalidad = nacionalidad;
        AniosExperiencia = aniosExperiencia;
        Email = email;
        Telefono = telefono;
    }

    public void ActualizarContacto(string nuevoEmail, string nuevoTelefono)
    {
        Email = nuevoEmail;
        Telefono = nuevoTelefono;
    }

    public override string ToString() => $"Chef: {NombreCompleto} ({Especialidad})";
}
