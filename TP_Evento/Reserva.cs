using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_Evento;

public class Reserva
{
    public int IdReserva { get; set; }
    public DateTime FechaReserva { get; set; }
    public bool Pagado { get; set; }
    public MetodoPago MetodoPago { get; set; }
    public EstadoReserva Estado { get; set; }
    public int IdEvento { get; set; }
    public EventoGastronomico Evento { get; set; }
    public int IdParticipante { get; set; }
    public Participante Participante { get; set; }
}