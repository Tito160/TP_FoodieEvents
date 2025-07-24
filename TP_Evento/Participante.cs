using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class Participante
{
    public int IdParticipante { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public string Documento { get; set; }
    public string Restriccion { get; set; }
}