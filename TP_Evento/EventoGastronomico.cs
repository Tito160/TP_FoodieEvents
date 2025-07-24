using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class EventoGastronomico
{
    public int IdEvento { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CapacidadMaxima { get; set; }
    public decimal Precio { get; set; }
    public string Ubicacion { get; set; }
    public int IdChef { get; set; }
    public TipoEvento Tipo { get; set; }
    public Chef Organizador { get; set; }
}