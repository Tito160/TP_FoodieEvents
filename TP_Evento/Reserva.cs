using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class Reserva
{
    public int Id { get; set; }
    public Participante Participante { get; set; }
    public EventoGastronomico Evento { get; set; }
    public DateTime FechaReserva { get; set; }
    public bool Pagado { get; set; }
    public string MetodoPago { get; set; } // tarjeta, transferencia, efectivo
    public string Estado { get; set; } // confirmada, cancelada, en espera

    // Métodos
    public void ConfirmarPago(string metodoPago);
    public void CancelarReserva();
    public void PonerEnEspera();
    public override string ToString(); // para mostrar datos resumidos de la reserva
}
