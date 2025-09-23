using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class Chef
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; }
    public string Especialidad { get; set; }
    public string Nacionalidad { get; set; }
    public int AniosExperiencia { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }

    // Métodos
    public void ActualizarContacto(string nuevoEmail, string nuevoTelefono);
    public override string ToString(); // para mostrar datos resumidos del chef
}
