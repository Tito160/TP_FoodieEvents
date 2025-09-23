using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class EventoGastronomico
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Tipo { get; set; } // cata, feria, clase, cena temática
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
    public int CapacidadMaxima { get; set; }
    public decimal PrecioPorEntrada { get; set; }
    public string Ubicacion { get; set; }
    
    // Relación
    public Chef Organizador { get; set; }
    public List<Reserva> Reservas { get; set; } = new List<Reserva>();

    // Métodos
    public bool HayCupoDisponible();
    public int LugaresDisponibles();
    public void AgregarReserva(Reserva reserva);
    public void CancelarReserva(Reserva reserva);
    public override string ToString(); // para mostrar datos resumidos del evento
}
