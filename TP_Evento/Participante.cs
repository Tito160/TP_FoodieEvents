using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class Participante
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
    public string DocumentoIdentidad { get; set; }
    public string RestriccionAlimentaria { get; set; } // opcional

    // Métodos
    public void ActualizarContacto(string nuevoEmail, string nuevoTelefono);
    public override string ToString(); // para mostrar datos resumidos del participante
}
